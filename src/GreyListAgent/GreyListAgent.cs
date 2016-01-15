namespace GreyListAgent
{
    using System;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.IO;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;
    using Microsoft.Exchange.Data.Mime;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using System.Threading;
    using NetTools;

    /// <summary>
    /// Agent for Greylisting
    /// </summary>
    public class GreyListAgent : SmtpReceiveAgent
    {
        /// <summary>
        /// The error message that will be sent to the client if
        /// you want to temporarily reject the message.
        /// </summary>
        private static readonly SmtpResponse DelayResponseMessage = new SmtpResponse(
                        "451",
                        "4.7.1",
                        "Greylisted. Try again later.");

        /// <summary>
        /// An instantiation of a class that can be used to convert
        /// strings to arrays of bytes. This is used in hash
        /// calculations.
        /// </summary>
        private static ASCIIEncoding asciiEncoding = new ASCIIEncoding();

        /// <summary>
        /// A reference to the server object.
        /// </summary>
        private SmtpServer server;

        /// <summary>
        /// A reference to a MailFilter settings object.
        /// </summary>
        private GreyListSettings settings;

        /// <summary>
        /// A flag that you will use to remember whether you want to
        /// run your algorithm after end of headers instead of RCPTCommand.
        /// </summary>
        private bool testOnEndOfHeaders;

        /// <summary>
        /// The database of verified entries.
        /// </summary>
        private GreyListDatabase greylistDatabase;

        /// <summary>
        /// A hash code generator that will be used to
        /// calculate the hash values of triplets.
        /// </summary>
        private SHA256Managed hashManager;

        /// <summary>
        /// Path to log file
        /// </summary>
        private String logPath;

        /// <summary>
        /// Initializes a Greylist agent for use
        /// </summary>
        /// <param name="settings">Settings object with populated settings</param>
        /// <param name="greylistDatabase">Greylist database to use for triplet management</param>
        /// <param name="hashManager">hash manager</param>
        /// <param name="server">Exchange server instance</param>
        public GreyListAgent(GreyListSettings settings, GreyListDatabase greylistDatabase, SHA256Managed hashManager, SmtpServer server, String logPath)
        {
            // Initialize instance variables.
            this.settings = settings;
            this.server = server;
            this.greylistDatabase = greylistDatabase;
            this.testOnEndOfHeaders = false;
            this.hashManager = hashManager;
            this.logPath = logPath;

            // Set up the hooks to have your functions called when certain events occur.
            this.OnRcptCommand += new RcptCommandEventHandler(this.OnRcptCommandHandler);
            this.OnEndOfHeaders += new EndOfHeadersEventHandler(this.OnEndOfHeaderHandler);
        }
        
        public void OnRcptCommandHandler(ReceiveCommandEventSource source, RcptCommandEventArgs rcptArgs)
        {
            // Check the parameter values.
            if (source == null || rcptArgs == null)
            {
                this.logLine("ERROR: Source or Recipient Argements was null", 1);
                return;
            }

            // Skip filtering for internal mail.
            if (!rcptArgs.SmtpSession.IsExternalConnection)
            {
                
                if (rcptArgs.RecipientAddress.ToString().IndexOf("HealthMailbox") == 0)
                {
                    return;
                }
                this.logLine("FROM=, TO=" + rcptArgs.RecipientAddress.ToString() + ", REMOTE=" + rcptArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Bypassed, REASON=Internal Connection", 2);
                return;
            }

            // If the sender domain is null, you will have to wait until
            // after the EndOfData event to check the message.
            if (RoutingAddress.NullReversePath.Equals(rcptArgs.MailItem.FromAddress))
            {
                this.testOnEndOfHeaders = true;
                return;
            }

            // Check to see if whitelisted or in safe senders list
            if (this.ShouldBypassFilter(rcptArgs.MailItem.FromAddress, rcptArgs.RecipientAddress, rcptArgs.SmtpSession.RemoteEndPoint.Address))
            {
                this.logLine("FROM=" + rcptArgs.MailItem.FromAddress.ToString() + ", TO=" + rcptArgs.RecipientAddress.ToString() + ", REMOTE=" + rcptArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Bypassed, REASON=Filter Bypass Match", 2);
                return;
            }

            // Check the database to determine whether the message should be rejected 
            // or let through.
            if (!this.VerifyTriplet(rcptArgs.SmtpSession.RemoteEndPoint.Address, rcptArgs.MailItem.FromAddress, rcptArgs.RecipientAddress))
            {
                this.logLine("FROM=" + rcptArgs.MailItem.FromAddress.ToString() + ", TO=" + rcptArgs.RecipientAddress.ToString() + ", REMOTE=" + rcptArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Greylist, REASON=Triplet verification Fail", 2);
                source.RejectCommand(DelayResponseMessage);
                return;
            }
            this.logLine("FROM=" + rcptArgs.MailItem.FromAddress.ToString() + ", TO=" + rcptArgs.RecipientAddress.ToString() + ", REMOTE=" + rcptArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Accept, REASON=Triplet Match.", 2);
                
            // Finally, check a few rows
            // for expired entries that need to be cleaned up.
            this.greylistDatabase.Clean(this.settings.CleanRowCount, this.settings.ConfirmedMaxAge, this.settings.UnconfirmedMaxAge);
        }
        
        public void OnEndOfHeaderHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs eodArgs)
        {
            if (this.testOnEndOfHeaders)
            {
                RoutingAddress senderAddress;
                // Reset the flag.
                this.testOnEndOfHeaders = false;

                // Get the sender address from the message header.
                Header fromAddress = eodArgs.Headers.FindFirst(HeaderId.From);
                if (fromAddress != null)
                {
                    senderAddress = new RoutingAddress(fromAddress.Value);
                }
                else
                {
                    this.logLine("FROM=, TO=Multiple, REMOTE=" + eodArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Greylist, REASON=No from address.", 2);                
                    // No sender address, reject the message.
                    source.RejectMessage(DelayResponseMessage);
                    return;
                }

                // Determine whether any of the recipients should be rejected, and if so, reject them all.
                bool rejectAll = false;
                foreach (EnvelopeRecipient currentRecipient in eodArgs.MailItem.Recipients)
                {
                    if (this.ShouldBypassFilter(senderAddress, currentRecipient.Address, eodArgs.SmtpSession.RemoteEndPoint.Address))
                    {
                        continue;
                    }
                    if (!this.VerifyTriplet(eodArgs.SmtpSession.RemoteEndPoint.Address, senderAddress, currentRecipient.Address))
                    {
                        this.logLine("FROM="+senderAddress.ToString()+", TO="+currentRecipient.Address.ToString()+", REMOTE=" + eodArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Greylist, REASON=Triplet verify failed.", 2);  
                        rejectAll = true;
                    }
                }

                if (rejectAll)
                {
                    this.logLine("FROM=" + senderAddress.ToString() + ", TO=MANY, REMOTE=" + eodArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Greylist, REASON=One or more recipients failed Triplet verification.", 2);                
                    source.RejectMessage(DelayResponseMessage);
                    return;
                }
                this.logLine("FROM=" + senderAddress.ToString() + ", TO=MANY, REMOTE=" + eodArgs.SmtpSession.RemoteEndPoint.Address.ToString() + ", STATE=Accept, REASON=Triplets Match.", 2);                
                    
            }
        }
        

        private bool VerifyTriplet(IPAddress remoteIP, RoutingAddress sender, RoutingAddress recipient)
        {

            String tripletHash = this.HashTriplet(remoteIP, sender.DomainPart, recipient.ToString());

            // Check to see if we are already aware of this hash
            if (this.greylistDatabase.Contains(tripletHash))
            {
                // We are aware of it. Check to see if it's already been confirmed
                if (((GreyListEntry)this.greylistDatabase[tripletHash]).Confirmed)
                {
                    // Update the last seen
                    ((GreyListEntry)this.greylistDatabase[tripletHash]).Count += 1;
                    ((GreyListEntry)this.greylistDatabase[tripletHash]).LastSeen = DateTime.UtcNow;
                    
                    // Go ahead and return true
                    return true;
                }

                // We are aware of it but it's not confirmed, Check to see if the blocking period has passed
                if (DateTime.UtcNow.Subtract(this.settings.GreylistingPeriod) > ((GreyListEntry)this.greylistDatabase[tripletHash]).LastSeen)
                {
                    ((GreyListEntry)this.greylistDatabase[tripletHash]).Confirmed = true;
                    ((GreyListEntry)this.greylistDatabase[tripletHash]).LastSeen = DateTime.UtcNow;
                    return true;
                }

                // We are aware and the blocking period has NOT passed.
                return false;
            }
            // We aren't aware of it, create an entry
            greylistDatabase.Add(tripletHash, new GreyListEntry(remoteIP, sender.DomainPart, recipient.ToString()));

            return false;

        }

        private bool ShouldBypassFilter(RoutingAddress sender, RoutingAddress recipient, IPAddress senderIP)
        {
            if (this.server == null || sender == null || recipient == null)
            {
                return false;
            }
            
            
            // Check Exchange bypasses
            AddressBook addressBook = this.server.AddressBook;
            if (addressBook != null)
            {
                AddressBookEntry addressBookEntry = addressBook.Find(recipient);
                if (addressBookEntry != null)
                {
                    if (addressBookEntry.AntispamBypass || addressBookEntry.IsSafeSender(sender) || addressBookEntry.IsSafeRecipient(recipient))
                    {
                        return true;
                    }
                }
            }

            // IPs (Can have netmask)
            foreach (String ip in this.settings.WhitelistIPs)
            {
                try
                {
                    IPAddressRange range = IPAddressRange.Parse(ip);
                    if (range.Contains(senderIP))
                    {
                        return true;
                    }
                }
                catch
                {
                    // Do Nothing
                }
            }

            // Do a reverse DNS lookup on the IP
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(senderIP);
                string reversedHostname = Reverse(hostInfo.HostName);
                foreach (String domain in this.settings.WhitelistClients)
                {
                    // Test for regex
                    if (domain.IndexOf('/') == 0)
                    {
                        try
                        {
                            Regex domaintest = new Regex(domain);
                            if (domaintest.IsMatch(hostInfo.HostName)) {
                                return true;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        continue;
                    }

                    // Test for a literal match
                    if (hostInfo.HostName == domain)
                    {
                        return true;
                    }

                    // Test for any subdomain match
                    string reversed = Reverse("."+domain);
                    if (reversedHostname.IndexOf(reversed) == 0) {
                        return true;
                    }
                }
            }
            catch
            {
                // Do nothing
            }

            return false;
        }

        private string HashTriplet(IPAddress senderIP, String senderDomain, String rcptAddress)
        {
            // A string that will contain an ASCII value of the triplet.
            String tripletString = String.Empty;

            // Append the IP address onto the triplet string.
            if (senderIP != null)
            {
                // Apply a netmask to any incoming IPs
                try 
                {

                    tripletString = string.Concat(tripletString, IPAddressRange.Parse(senderIP.ToString() + "/" + this.settings.IpNetmask.ToString()).Begin.ToString());
                }
                catch
                {
                    tripletString = string.Concat(tripletString, senderIP.ToString());
                }
            }

            // Append the recipient's address onto the triplet string.
            if (rcptAddress != null)
            {
                tripletString = string.Concat(tripletString, rcptAddress);
            }

            // Append the sender's domain onto the triplet string.
            if (senderDomain != null)
            {
                tripletString = string.Concat(tripletString, senderDomain);
            }

            // Convert the string to lowercase and get its value as a byte[].
            Byte[] hashInput = asciiEncoding.GetBytes(tripletString.ToLowerInvariant());

            // Calculate the SHA256 hash.
            Byte[] hashResult;

            // Lock the hash manager for usage
            lock (this.hashManager)
            {
                hashResult = this.hashManager.ComputeHash(hashInput);
            }

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hashResult.Length; i++)
            {
                sBuilder.Append(hashResult[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool TestVerify(IPAddress senderIP, RoutingAddress sender, RoutingAddress recipient)
        {
            return this.VerifyTriplet(senderIP, sender, recipient);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void logLine(string message, int level)
        {
            // Don't log it if we dont want it
            if (level > this.settings.LogLevel)
            {
                return;
            }

            // 10 tries 100ms apart
            int tries = 10;
            while (tries > 0)
            {
                try
                {
                    if (!File.Exists(this.logPath))
                    {
                        File.CreateText(this.logPath).Close();
                    }
                    StreamWriter log = File.AppendText(this.logPath);
                    log.WriteLine("[" + DateTime.Now.ToString("g", DateTimeFormatInfo.InvariantInfo) + "] " + message);
                    log.Close();
                    tries = 0;
                }
                catch
                {
                    tries -= 1;
                    Thread.Sleep(100);
                }
            }

        }
    }
}
