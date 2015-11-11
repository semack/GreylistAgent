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

        public GreyListEntry()
        {
            this.senderIP = null;
            this.senderDomain = null;
            this.rcptAddress = null;
            this.firstSeen = DateTime.UtcNow;
            this.lastSeen = DateTime.UtcNow;
            this.confirmed = false;
        }
        public GreyListEntry(IPAddress senderIP, string senderDomain, string rcptAddress)
        {
            this.senderIP = senderIP;
            this.senderDomain = senderDomain;
            this.rcptAddress = rcptAddress;
            this.firstSeen = DateTime.UtcNow;
            this.lastSeen = DateTime.UtcNow;
            this.confirmed = false;
        }

        public Boolean Confirmed
        {
            get { return this.confirmed; }
            set { this.confirmed = value; }
        }

        public DateTime FirstSeen
        {
            get { return this.firstSeen; }
            set { this.firstSeen = value; }
        }

        public DateTime LastSeen
        {
            get { return this.lastSeen; }
            set { this.lastSeen = value; }
        }

        public String SenderIP
        {
            get { return this.senderIP == null ? null : this.senderIP.ToString(); }
            set { this.senderIP = string.IsNullOrEmpty(value) ? null : IPAddress.Parse(value); }
        }

        public String SenderDomain
        {
            get { return this.senderDomain; }
            set { this.senderDomain = value; }
        }

        public String RcptAddress
        {
            get { return this.rcptAddress; }
            set { this.rcptAddress = value; }
        }
    }
}