using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using NetTools;

namespace GreyListAgent
{
    /// <summary>
    ///     Agent for Greylisting
    /// </summary>
    public class GreyListAgent : SmtpReceiveAgent
    {
        /// <summary>
        ///     The error message that will be sent to the client if
        ///     you want to temporarily reject the message.
        /// </summary>
        private static readonly SmtpResponse DelayResponseMessage =
#if EX2016RTM
            SmtpResponse.CreateWithDsnExplanation
#else
        new SmtpResponse
#endif
                ("451",
                    "4.7.1",
                    "Greylisted. Try again later.");

        /// <summary>
        ///     An instantiation of a class that can be used to convert
        ///     strings to arrays of bytes. This is used in hash
        ///     calculations.
        /// </summary>
        private static readonly ASCIIEncoding AsciiEncoding = new ASCIIEncoding();

        /// <summary>
        ///     The database of verified entries.
        /// </summary>
        private readonly GreyListDatabase _greylistDatabase;

        /// <summary>
        ///     A hash code generator that will be used to
        ///     calculate the hash values of triplets.
        /// </summary>
        private readonly SHA256Managed _hashManager;

        /// <summary>
        ///     Using log4net as logger
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        ///     A reference to the _server object.
        /// </summary>
        private readonly SmtpServer _server;

        /// <summary>
        ///     A reference to a MailFilter _settings object.
        /// </summary>
        private readonly GreyListSettings _settings;

        /// <summary>
        ///     A flag that you will use to remember whether you want to
        ///     run your algorithm after end of headers instead of RCPTCommand.
        /// </summary>
        private bool _testOnEndOfHeaders;

        /// <summary>
        ///     Initializes a Greylist agent for use
        /// </summary>
        /// <param name="settings">Settings object with populated _settings</param>
        /// <param name="greylistDatabase">Greylist database to use for triplet management</param>
        /// <param name="hashManager">hash manager</param>
        /// <param name="server">Exchange _server instance</param>
        /// <param name="log"></param>
        public GreyListAgent(GreyListSettings settings, GreyListDatabase greylistDatabase, SHA256Managed hashManager,
            SmtpServer server, ILog log)
        {
            // Initialize instance variables.
            _settings = settings;
            _server = server;
            _greylistDatabase = greylistDatabase;
            _testOnEndOfHeaders = false;
            _hashManager = hashManager;
            _log = log;

            // Set up the hooks to have your functions called when certain events occur.
            OnRcptCommand += OnRcptCommandHandler;
            OnEndOfHeaders += OnEndOfHeaderHandler;
        }

        public void OnRcptCommandHandler(ReceiveCommandEventSource source, RcptCommandEventArgs rcptArgs)
        {
            // Check the parameter values.
            if (source == null || rcptArgs == null)
            {
                _log.Error("ERROR: Source or Recipient Argements was null");
                return;
            }

            // Skip filtering for internal mail.
            if (!rcptArgs.SmtpSession.IsExternalConnection)
            {
                if (rcptArgs.RecipientAddress.ToString().IndexOf("HealthMailbox", StringComparison.Ordinal) == 0)
                {
                    return;
                }
                _log.InfoFormat("FROM=, TO={0}, REMOTE={1}, STATE=Bypassed, REASON=Internal Connection",
                    rcptArgs.RecipientAddress, rcptArgs.SmtpSession.RemoteEndPoint.Address);
                return;
            }

            // If the sender domain is null, you will have to wait until
            // after the EndOfData event to check the message.
            if (RoutingAddress.NullReversePath.Equals(rcptArgs.MailItem.FromAddress))
            {
                _testOnEndOfHeaders = true;
                return;
            }

            // Check to see if whitelisted or in safe senders list
            if (ShouldBypassFilter(rcptArgs.MailItem.FromAddress, rcptArgs.RecipientAddress,
                rcptArgs.SmtpSession.RemoteEndPoint.Address))
            {
                _log.InfoFormat("FROM={0}, TO={1}, REMOTE={2}, STATE=Bypassed, REASON=Filter Bypass Match",
                    rcptArgs.MailItem.FromAddress, rcptArgs.RecipientAddress,
                    rcptArgs.SmtpSession.RemoteEndPoint.Address);
                return;
            }

            // Check the database to determine whether the message should be rejected 
            // or let through.
            if (
                !VerifyTriplet(rcptArgs.SmtpSession.RemoteEndPoint.Address, rcptArgs.MailItem.FromAddress,
                    rcptArgs.RecipientAddress))
            {
                _log.InfoFormat("FROM={0}, TO={1}, REMOTE={2}, STATE=Greylist, REASON=Triplet verification Fail",
                    rcptArgs.MailItem.FromAddress, rcptArgs.RecipientAddress,
                    rcptArgs.SmtpSession.RemoteEndPoint.Address);
                source.RejectCommand(DelayResponseMessage);
                return;
            }
            _log.InfoFormat("FROM={0}, TO={1}, REMOTE={2}, STATE=Accept, REASON=Triplet Match.",
                rcptArgs.MailItem.FromAddress, rcptArgs.RecipientAddress, rcptArgs.SmtpSession.RemoteEndPoint.Address);

            // Finally, check a few rows
            // for expired entries that need to be cleaned up.
            _greylistDatabase.Clean(_settings.CleanRowCount, _settings.ConfirmedMaxAge, _settings.UnconfirmedMaxAge);
        }

        public void OnEndOfHeaderHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs eodArgs)
        {
            if (_testOnEndOfHeaders)
            {
                RoutingAddress senderAddress;
                // Reset the flag.
                _testOnEndOfHeaders = false;

                // Get the sender address from the message header.
                var fromAddress = eodArgs.Headers.FindFirst(HeaderId.From);
                if (fromAddress != null)
                {
                    senderAddress = new RoutingAddress(fromAddress.Value);
                }
                else
                {
                    _log.InfoFormat("FROM=, TO=Multiple, REMOTE={0}, STATE=Greylist, REASON=No from address.",
                        eodArgs.SmtpSession.RemoteEndPoint.Address);
                    // No sender address, reject the message.
                    source.RejectMessage(DelayResponseMessage);
                    return;
                }

                // Determine whether any of the recipients should be rejected, and if so, reject them all.
                var rejectAll = false;
                foreach (var currentRecipient in eodArgs.MailItem.Recipients)
                {
                    if (ShouldBypassFilter(senderAddress, currentRecipient.Address,
                        eodArgs.SmtpSession.RemoteEndPoint.Address))
                    {
                        continue;
                    }
                    if (
                        !VerifyTriplet(eodArgs.SmtpSession.RemoteEndPoint.Address, senderAddress,
                            currentRecipient.Address))
                    {
                        _log.InfoFormat("FROM={0}, TO={1}, REMOTE={2}, STATE=Greylist, REASON=Triplet verify failed.",
                            senderAddress, currentRecipient.Address, eodArgs.SmtpSession.RemoteEndPoint.Address);
                        rejectAll = true;
                    }
                }

                if (rejectAll)
                {
                    _log.InfoFormat(
                        "FROM={0}, TO=MANY, REMOTE={1}, STATE=Greylist, REASON=One or more recipients failed Triplet verification.",
                        senderAddress, eodArgs.SmtpSession.RemoteEndPoint.Address);
                    source.RejectMessage(DelayResponseMessage);
                    return;
                }
                _log.InfoFormat("FROM={0}, TO=MANY, REMOTE={1}, STATE=Accept, REASON=Triplets Match.",
                    senderAddress, eodArgs.SmtpSession.RemoteEndPoint.Address);
            }
        }


        private bool VerifyTriplet(IPAddress remoteIp, RoutingAddress sender, RoutingAddress recipient)
        {
            var tripletHash = HashTriplet(remoteIp, sender.DomainPart, recipient.ToString());

            // Check to see if we are already aware of this hash
            if (_greylistDatabase.Contains(tripletHash))
            {
                var entry = (GreyListEntry) _greylistDatabase[tripletHash];
                // We are aware of it. Check to see if it's already been confirmed
                if (entry.Confirmed)
                {
                    // Update the last seen
                    entry.Count += 1;
                    entry.LastSeen = DateTime.UtcNow;

                    // Go ahead and return true
                    return true;
                }

                // We are aware of it but it's not confirmed, Check to see if the blocking period has passed
                if (DateTime.UtcNow.Subtract(_settings.GreylistingPeriod) > entry.LastSeen)
                {
                    entry.Confirmed = true;
                    entry.LastSeen = DateTime.UtcNow;
                    return true;
                }

                // We are aware and the blocking period has NOT passed.
                return false;
            }
            // We aren't aware of it, create an entry
            _greylistDatabase.Add(tripletHash, new GreyListEntry(remoteIp, sender.DomainPart, recipient.ToString()));

            return false;
        }

        private bool ShouldBypassFilter(RoutingAddress sender, RoutingAddress recipient, IPAddress senderIp)
        {
            if (_server == null || sender == null || recipient == null)
            {
                return false;
            }


            // Check Exchange bypasses
            var addressBook = _server.AddressBook;
            var addressBookEntry = addressBook?.Find(recipient);
            if (addressBookEntry != null)
            {
                if (addressBookEntry.AntispamBypass || addressBookEntry.IsSafeSender(sender) ||
                    addressBookEntry.IsSafeRecipient(recipient))
                {
                    return true;
                }
            }

            // IPs (Can have netmask)
            foreach (var ip in _settings.WhitelistIPs)
            {
                try
                {
                    var range = IPAddressRange.Parse(ip);
                    if (range.Contains(senderIp))
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
                var hostInfo = Dns.GetHostEntry(senderIp);
                var reversedHostname = Reverse(hostInfo.HostName);
                foreach (var domain in _settings.WhitelistClients)
                {
                    // Test for regex
                    if (domain.IndexOf('/') == 0)
                    {
                        try
                        {
                            var domaintest = new Regex(domain);
                            if (domaintest.IsMatch(hostInfo.HostName))
                            {
                                return true;
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                        continue;
                    }

                    // Test for a literal match
                    if (hostInfo.HostName == domain)
                    {
                        return true;
                    }

                    // Test for any subdomain match
                    var reversed = Reverse("." + domain);
                    if (reversedHostname.IndexOf(reversed, StringComparison.Ordinal) == 0)
                    {
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

        private string HashTriplet(IPAddress senderIp, string senderDomain, string rcptAddress)
        {
            // A string that will contain an ASCII value of the triplet.
            var tripletString = string.Empty;

            // Append the IP address onto the triplet string.
            if (senderIp != null)
            {
                // Apply a netmask to any incoming IPs
                try
                {
                    tripletString = string.Concat(tripletString,
                        IPAddressRange.Parse(senderIp + "/" + _settings.IpNetmask).Begin.ToString());
                }
                catch
                {
                    tripletString = string.Concat(tripletString, senderIp.ToString());
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
            var hashInput = AsciiEncoding.GetBytes(tripletString.ToLowerInvariant());

            // Calculate the SHA256 hash.
            byte[] hashResult;

            // Lock the hash manager for usage
            lock (_hashManager)
            {
                hashResult = _hashManager.ComputeHash(hashInput);
            }

            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (byte t in hashResult)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool TestVerify(IPAddress senderIp, RoutingAddress sender, RoutingAddress recipient)
        {
            return VerifyTriplet(senderIp, sender, recipient);
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}