namespace GreyListAgent
{
    using System;
    using System.Net;

    public class GreyListEntry
    {
        private IPAddress senderIP = null;

        /// <summary>
        /// Initializes a default and empty Greylist entry
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
        /// Initializes a Greylist entry with triplet data
        /// </summary>
        /// <param name="senderIP"></param>
        /// <param name="senderDomain"></param>
        /// <param name="rcptAddress"></param>
        public GreyListEntry(IPAddress senderIP, string senderDomain, string rcptAddress)
        {
            this.senderIP = senderIP;
            SenderDomain = senderDomain;
            RcptAddress = rcptAddress;
            FirstSeen = DateTime.UtcNow;
            LastSeen = DateTime.UtcNow;
            Confirmed = false;
            Count = 1;
        }

        /// <summary>
        /// Number of times this triplet has been seen
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Is the triplet confirmed or not 
        /// (has the greylisting period passed and the triplet been seen again?)
        /// </summary>
        public Boolean Confirmed { get; set; }

        /// <summary>
        /// First time the triplet was seen
        /// </summary>
        public DateTime FirstSeen { get; set; }

        /// <summary>
        /// Last time the triplet was seen
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        /// Sender IP address
        /// </summary>
        public String SenderIP
        {
            get { return this.senderIP == null ? null : this.senderIP.ToString(); }
            set { this.senderIP = string.IsNullOrEmpty(value) ? null : IPAddress.Parse(value); }
        }

        /// <summary>
        /// Sender domain name
        /// </summary>
        public String SenderDomain { get; set; }

        /// <summary>
        /// Recipient address
        /// </summary>
        public String RcptAddress { get; set; }
    }
}