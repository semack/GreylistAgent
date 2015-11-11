namespace GreyListAgent
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    using Microsoft.Exchange.Data;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;
    using Microsoft.Exchange.Data.Mime;


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
                        "Greylisted. Try again Later.");

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
        /// The IP address of the sending SMTP host as
        /// recorded in the SMTP envelope.
        /// </summary>
        private IPAddress senderIP;

        /// <summary>
        /// The email address of the message sender as
        /// recorded in the SMTP envelope.
        /// </summary>
        private RoutingAddress senderAddress;

        /// <summary>
        /// The email address of the message recipient as
        /// recorded in the SMTP envelope.
        /// </summary> 
        private RoutingAddress recipientAddress;

        /// <summary>
        /// The database of verified entries.
        /// </summary>
        private GreyListDatabase greylistDatabase;

        /// <summary>
        /// A hash code generator that will be used to
        /// calculate the hash values of triplets.
        /// </summary>
        private SHA256Managed hashManager;

       
        public GreyListAgent(
                             GreyListSettings settings,
                             GreyListDatabase greylistDatabase,
                             SHA256Managed hashManager,
                             SmtpServer server)
        {
            // Initialize instance variables.
            this.settings = settings;
            this.server = server;
            this.greylistDatabase = greylistDatabase;
            this.testOnEndOfHeaders = false;
            this.hashManager = hashManager;

            // Set up the hooks to have your functions called when certain events occur.
            this.OnRcptCommand += new RcptCommandEventHandler(this.OnRcptCommandHandler);
            this.OnEndOfHeaders += new EndOfHeadersEventHandler(this.OnEndOfHeaderHandler);
        }
        
        public void OnRcptCommandHandler(ReceiveCommandEventSource source, RcptCommandEventArgs rcptArgs)
        {
            // Check the parameter values.
            if (source == null || rcptArgs == null)
            {
                return;
            }

            // Skip filtering for internal mail.
            if (!rcptArgs.SmtpSession.IsExternalConnection)
            {
                return;
            }

            // Retrieve data used to identify this message.
            this.senderAddress = rcptArgs.MailItem.FromAddress;
            this.senderIP = rcptArgs.SmtpSession.RemoteEndPoint.Address;
            this.recipientAddress = rcptArgs.RecipientAddress;

            // If the sender domain is null, you will have to wait until
            // after the EndOfData event to check the message.
            if (RoutingAddress.NullReversePath.Equals(this.senderAddress))
            {
                this.testOnEndOfHeaders = true;
                return;
            }

            // Skip temporary blocking for safe senders.
            if (this.ShouldBypassFilter(this.senderAddress, this.recipientAddress, this.server))
            {
                return;
            }

            // Check the database to determine whether the message should be rejected 
            // or let through.
            if (!this.VerifyTriplet(this.senderIP, this.senderAddress, this.recipientAddress))
            {
                source.RejectCommand(DelayResponseMessage);
            }

            // Finally, check a few rows
            // for expired entries that need to be cleaned up.
            this.greylistDatabase.Clean(
                                                   this.settings.CleanRowCount,
                                                   this.settings.ConfirmedMaxAge,
                                                   this.settings.UnconfirmedMaxAge);
        }
        
        public void OnEndOfHeaderHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs eodArgs)
        {
            if (this.testOnEndOfHeaders)
            {
                // Reset the flag.
                this.testOnEndOfHeaders = false;

                // Get the sender address from the message header.
                Header fromAddress = eodArgs.Headers.FindFirst(HeaderId.From);
                if (fromAddress != null)
                {
                    this.senderAddress = new RoutingAddress(fromAddress.Value);
                }
                else
                {
                    // No sender address, reject the message.
                    source.RejectMessage(DelayResponseMessage);
                    return;
                }

                // Determine whether any of the recipients should be rejected, and if so, reject them all.
                bool rejectAll = false;
                foreach (EnvelopeRecipient currentRecipient in eodArgs.MailItem.Recipients)
                {
                    if (!this.ShouldBypassFilter(this.senderAddress, currentRecipient.Address, this.server) &&
                        !this.VerifyTriplet(this.senderIP, this.senderAddress, currentRecipient.Address))
                    {
                        rejectAll = true;
                    }
                }

                if (rejectAll)
                {
                    source.RejectMessage(DelayResponseMessage);
                }
            }
        }
        

        private bool VerifyTriplet(IPAddress remoteIP, RoutingAddress sender, RoutingAddress recipient)
        {
            String tripletHash = this.HashTriplet(
                                                  remoteIP,
                                                  sender.DomainPart,
                                                  recipient.ToString());

            // Check to see if we are already aware of this hash
            if (this.greylistDatabase.Contains(tripletHash))
            {
                // We are aware of it. Check to see if it's already been confirmed
                if (((GreyListEntry)this.greylistDatabase[tripletHash]).Confirmed)
                {
                    // Update the last seen
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

        private bool ShouldBypassFilter(RoutingAddress sender, RoutingAddress recipient, SmtpServer server)
        {
            if (server == null || sender == null || recipient == null)
            {
                return false;
            }

            AddressBook addressBook = server.AddressBook;
            if (addressBook != null)
            {
                AddressBookEntry addressBookEntry = addressBook.Find(recipient);
                if (addressBookEntry != null)
                {
                    if (addressBookEntry.AntispamBypass ||
                        addressBookEntry.IsSafeSender(sender) ||
                        addressBookEntry.IsSafeRecipient(recipient))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string HashTriplet(IPAddress senderIP, string senderDomain, string rcptAddress)
        {
            // A string that will contain an ASCII value of the triplet.
            string tripletString = String.Empty;

            // Append the IP address onto the triplet string.
            if (senderIP != null)
            {
                tripletString = string.Concat(tripletString, senderIP.ToString());
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
            byte[] hashInput = asciiEncoding.GetBytes(tripletString.ToLowerInvariant());

            // Calculate the SHA256 hash.
            byte[] hashResult;
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
    }
}
