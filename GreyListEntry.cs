namespace GreyListAgent
{
    using System;
    using System.Net;

    public class GreyListEntry
    {
        private DateTime firstSeen;
        private DateTime lastSeen;
        private IPAddress senderIP;
        private String senderDomain;
        private String rcptAddress;
        private Boolean confirmed;
        private int count;

        /// <summary>
        /// Initializes a default and empty Greylist entry
        /// </summary>
        public GreyListEntry()
        {
            this.senderIP = null;
            this.senderDomain = null;
            this.rcptAddress = null;
            this.firstSeen = DateTime.UtcNow;
            this.lastSeen = DateTime.UtcNow;
            this.confirmed = false;
            this.count = 1;
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
            this.senderDomain = senderDomain;
            this.rcptAddress = rcptAddress;
            this.firstSeen = DateTime.UtcNow;
            this.lastSeen = DateTime.UtcNow;
            this.confirmed = false;
            this.count = 1;
        }

        /// <summary>
        /// Number of times this triplet has been seen
        /// </summary>
        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        /// <summary>
        /// Is the triplet confirmed or not 
        /// (has the greylisting period passed and the triplet been seen again?)
        /// </summary>
        public Boolean Confirmed
        {
            get { return this.confirmed; }
            set { this.confirmed = value; }
        }

        /// <summary>
        /// First time the triplet was seen
        /// </summary>
        public DateTime FirstSeen
        {
            get { return this.firstSeen; }
            set { this.firstSeen = value; }
        }

        /// <summary>
        /// Last time the triplet was seen
        /// </summary>
        public DateTime LastSeen
        {
            get { return this.lastSeen; }
            set { this.lastSeen = value; }
        }

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
        public String SenderDomain
        {
            get { return this.senderDomain; }
            set { this.senderDomain = value; }
        }

        /// <summary>
        /// Recipient address
        /// </summary>
        public String RcptAddress
        {
            get { return this.rcptAddress; }
            set { this.rcptAddress = value; }
        }
    }
}