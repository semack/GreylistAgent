namespace GreyListAgent
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using System.Xml;

    [Serializable]
    [XmlRoot("GreyListConfig")]
    public class GreyListSettings
    {
        /// <summary>
        /// Number of rows to clean on each recieve 
        /// </summary>
        public int CleanRowCount { get; set; }

        /// <summary>
        /// Maximum age of unconfirmed triplets before getting cleaned up.
        /// </summary>
        [XmlIgnore]
        public TimeSpan GreylistingPeriod { get; set; }

        [XmlElement("GreylistingPeriod")]
        public string GreylistingPeriodValue
        {
            get { return GreylistingPeriod.ToString(); }
            set { GreylistingPeriod = string.IsNullOrEmpty(value) ? TimeSpan.Zero : TimeSpan.Parse(value); }
        }

        /// <summary>
        /// Maximum age of confirmed triplets before requring re-confirmation and getting cleaned up.
        /// </summary>
        [XmlIgnore]
        public TimeSpan ConfirmedMaxAge { get; set; }

        [XmlElement("ConfirmedMaxAge")]
        public string ConfirmedMaxAgeValue
        {
            get { return ConfirmedMaxAge.ToString(); }
            set { ConfirmedMaxAge = string.IsNullOrEmpty(value) ? TimeSpan.Zero : TimeSpan.Parse(value); }
        }

        /// <summary>
        /// Maximum age of unconfirmed triplets before getting cleaned up.
        /// </summary>
        [XmlIgnore]
        public TimeSpan UnconfirmedMaxAge { get; set; }

        [XmlElement("UnconfirmedMaxAge")]
        public string UnconfirmedMaxAgeValue
        {
            get { return UnconfirmedMaxAge.ToString(); }
            set { UnconfirmedMaxAge = string.IsNullOrEmpty(value) ? TimeSpan.Zero : TimeSpan.Parse(value); }
        }

        /// <summary>
        /// IP Netmask to apply to source IPs when hashing triplets
        /// </summary>
        public int IpNetmask { get; set; }

        /// <summary>
        /// List of IPs to whitelist (Can contain a subnet mask or CIDR)
        /// </summary>
        [XmlArrayItem(ElementName = "IP")]
        public List<string> WhitelistIPs { get; set; }

        /// <summary>
        /// List of rDNS records for clients (sending servers) to whitelist.
        /// Regex is acceptable (must be encased in starting and ending slashes)
        /// Non-Regex matches attempt a literal match and a wildcard subdomain match
        /// </summary>
        [XmlArrayItem(ElementName = "Client")]
        public List<string> WhitelistClients { get; set; }

        /// <summary>
        /// An empty constructor initializes with default values.
        /// </summary>
        /// <param name="path">The path to an XML file that contains the settings.</param>
        private GreyListSettings()
        {
            // Default of 30 days
            ConfirmedMaxAge = new TimeSpan(30, 0, 0, 0);

            // Default of 4 hours
            UnconfirmedMaxAge = new TimeSpan(0, 4, 0, 0);

            // Defaut of 5 minutes
            GreylistingPeriod = new TimeSpan(0, 0, 5, 0);

            // Default of 100 rows per pass
            CleanRowCount = 100;

            // Default of a /24 or 255.255.255.0 when hashing
            IpNetmask = 24;

            // Default empty whitelists
            WhitelistClients = new List<string>();
            WhitelistIPs = new List<string>();
        }

        public static GreyListSettings Load(string path)
        {
            var retval = new GreyListSettings();
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GreyListSettings));
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    retval = serializer.Deserialize(stream) as GreyListSettings;
                }
            }
            return retval;
        }

        public void Save(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GreyListSettings));
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
