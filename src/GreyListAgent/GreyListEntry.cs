using System;
using System.Net;

namespace GreyListAgent
{
    public class GreyListEntry
    {
        private IPAddress _senderIp;

        /// <summary>
        ///     Initializes a default and empty Greylist entry
        /// </summary>
        public GreyListEntry()
        {
            SenderDomain = null;
            RcptAddress = null;
            FirstSeen = DateTime.UtcNow;
            LastSeen = DateTime.UtcNow;
            Confirmed = false;
            Count = 1;
        }

        /// <summary>
        ///     Initializes a Greylist entry with triplet data
        /// </summary>
        /// <param name="senderIp"></param>
        /// <param name="senderDomain"></param>
        /// <param name="rcptAddress"></param>
        public GreyListEntry(IPAddress senderIp, string senderDomain, string rcptAddress)
        {
            _senderIp = senderIp;
            SenderDomain = senderDomain;
            RcptAddress = rcptAddress;
            FirstSeen = DateTime.UtcNow;
            LastSeen = DateTime.UtcNow;
            Confirmed = false;
            Count = 1;
        }

        /// <summary>
        ///     Number of times this triplet has been seen
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     Is the triplet confirmed or not
        ///     (has the greylisting period passed and the triplet been seen again?)
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        ///     First time the triplet was seen
        /// </summary>
        public DateTime FirstSeen { get; set; }

        /// <summary>
        ///     Last time the triplet was seen
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        ///     Sender IP address
        /// </summary>
        public string SenderIp
        {
            get { return _senderIp?.ToString(); }
            set { _senderIp = string.IsNullOrEmpty(value) ? null : IPAddress.Parse(value); }
        }

        /// <summary>
        ///     Sender domain name
        /// </summary>
        public string SenderDomain { get; set; }

        /// <summary>
        ///     Recipient address
        /// </summary>
        public string RcptAddress { get; set; }
    }
}