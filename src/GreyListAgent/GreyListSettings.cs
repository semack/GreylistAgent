namespace GreyListAgent
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections.Generic;

    public class GreyListSettings
    {
        private TimeSpan confirmedMaxAge;
        private TimeSpan unconfirmedMaxAge;
        private TimeSpan greylistingPeriod;
        private int cleanRowCount;
        private List<string> whitelistClients;
        private List<string> whitelistIPs;
        private int ipNetmask;

        /// <summary>
        /// An empty constructor initializes with default values.
        /// </summary>
        /// <param name="path">The path to an XML file that contains the settings.</param>
        public GreyListSettings(string path)
        {
            // Default of 30 days
            this.confirmedMaxAge = new TimeSpan(30, 0, 0, 0);

            // Default of 4 hours
            this.unconfirmedMaxAge = new TimeSpan(0, 4, 0, 0);

            // Defaut of 5 minutes
            this.greylistingPeriod = new TimeSpan(0, 0, 5, 0);

            // Default of 100 rows per pass
            this.cleanRowCount = 100;

            // Default of a /24 or 255.255.255.0 when hashing
            this.ipNetmask = 24;

            // Default empty whitelists
            this.whitelistClients = new List<string>();
            this.whitelistIPs = new List<string>();

            // Read configured options
            this.ReadXMLConfig(path);
 
        }

        public GreyListSettings(GreyListSettings other)
        {
            this.ConfirmedMaxAge = other.ConfirmedMaxAge;
            this.UnconfirmedMaxAge = other.UnconfirmedMaxAge;
            this.GreylistingPeriod = other.GreylistingPeriod;
            this.CleanRowCount = other.CleanRowCount;
            this.IpNetmask = other.IpNetmask;
            this.WhitelistClients = other.WhitelistClients;
            this.WhitelistIPs = other.WhitelistIPs;
        }

        /// <summary>
        /// Maximum age of confirmed triplets before requring re-confirmation and getting cleaned up.
        /// </summary>
        public TimeSpan ConfirmedMaxAge
        {
            get { return this.confirmedMaxAge; }

            set { this.confirmedMaxAge = value; }
        }

        /// <summary>
        /// Maximum age of unconfirmed triplets before getting cleaned up.
        /// </summary>
        public TimeSpan UnconfirmedMaxAge
        {
            get { return this.unconfirmedMaxAge; }

            set { this.unconfirmedMaxAge = value; }
        }

        /// <summary>
        /// Period of time a triplet is greylisted 
        /// </summary>
        public TimeSpan GreylistingPeriod
        {
            get { return this.greylistingPeriod; }

            set { this.greylistingPeriod = value; }
        }

        /// <summary>
        /// Number of rows to clean on each recieve 
        /// </summary>
        public int CleanRowCount
        {
            get { return this.cleanRowCount; }

            set { this.cleanRowCount = value; }
        }

        /// <summary>
        /// IP Netmask to apply to source IPs when hashing triplets
        /// </summary>
        public int IpNetmask
        {
            get { return this.ipNetmask; }

            set { this.ipNetmask = value; }
        }

        /// <summary>
        /// List of IPs to whitelist (Can contain a subnet mask or CIDR)
        /// </summary>
        public List<string> WhitelistIPs
        {
            get { return this.whitelistIPs; }
            set { this.whitelistIPs = value; }
        }

        /// <summary>
        /// List of rDNS records for clients (sending servers) to whitelist.
        /// Regex is acceptable (must be encased in starting and ending slashes)
        /// Non-Regex matches attempt a literal match and a wildcard subdomain match
        /// </summary>
        public List<string> WhitelistClients
        {
            get { return this.whitelistClients; }
            set { this.whitelistClients = value; }
        }
       
        #region XML File Parsing
        /// <summary>
        /// Reads in configuration options from an XML file and sets the instance
        /// variables to the corresponding values that are read in if they
        /// are valid. If an invalid value is found, or a value is not
        /// set in the XML file, the variable will not be changed from its
        /// default value.
        /// </summary>
        /// <param name="path">The path to the XML configuration file.</param>
        /// <returns>True if the file was read.</returns>
        public bool ReadXMLConfig(string path)
        {
            bool retval = false;

            try
            {
                // Some temp variables that will be used during validation.
                int fileInt = 0;
                TimeSpan fileTime = new TimeSpan();
                List<string> fileStrings = new List<string>();

                // Load the file into the XML reader.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNode xmlRoot = xmlDoc.SelectSingleNode("GreyListConfig");

                // Read in the number of rows to clean in each instance.
                fileInt = this.ReadXmlInt(xmlRoot, "CleanRowCount");
                if (fileInt > 0)
                {
                    this.cleanRowCount = fileInt;
                }

                // Read in the IP netmask
                fileInt = this.ReadXmlInt(xmlRoot, "IpNetmask");
                if (fileInt > 0)
                {
                    this.ipNetmask = fileInt;
                }

                // Read in the initial blocking period.
                fileTime = this.ReadXmlTimeSpan(xmlRoot, "GreylistingPeriod");
                if (fileTime > new TimeSpan())
                {
                    this.greylistingPeriod = fileTime;
                }

                // Read in the verified entry lifetime.
                fileTime = this.ReadXmlTimeSpan(xmlRoot, "ConfirmedMaxAge");
                if (fileTime > new TimeSpan())
                {
                    this.confirmedMaxAge = fileTime;
                }

                // Read in the unverified entry lifetime.
                fileTime = this.ReadXmlTimeSpan(xmlRoot, "UnconfirmedMaxAge");
                if (fileTime > new TimeSpan())
                {
                    this.unconfirmedMaxAge = fileTime;
                }

                // Read whitelisted IPs
                fileStrings = this.ReadXmlStringList(xmlRoot, "WhitelistIPs");
                if (fileStrings.Count > 0)
                {
                    this.whitelistIPs = fileStrings;
                }

                // Read whitelisted clients
                fileStrings = this.ReadXmlStringList(xmlRoot, "WhitelistClients");
                if (fileStrings.Count > 0)
                {
                    this.whitelistClients = fileStrings;
                }

            }
            catch (XmlException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            return retval;
        }

        /// <summary>
        /// Reads a list of strings based on the element name and return them
        /// </summary>
        /// <param name="root">The root element to start searching from.</param>
        /// <param name="xmlParam">The name of the element to get the value list of.</param>
        /// <returns>A list of strings.</returns>
        private List<String> ReadXmlStringList(XmlNode root, string xmlParam)
        {
            List<String> retval = new List<string>();

            if (root != null && xmlParam != null)
            {
                XmlNode valNode = root.SelectSingleNode(xmlParam);
                if (valNode != null)
                {
                    foreach (XmlNode childNode in valNode.ChildNodes)
                    {
                        retval.Add(childNode.InnerText.Trim());
                    }
                }
            }
            return retval;
        }

        /// <summary>
        /// Reads an integer value based on the name of the element it is the value of.
        /// </summary>
        /// <param name="root">The root element to start searching from.</param>
        /// <param name="xmlParam">The name of the element to get the value of.</param>
        /// <returns>The parsed value, or 0 if it was not found.</returns>
        private int ReadXmlInt(XmlNode root, string xmlParam)
        {
            int retval = 0;

            if (root != null && xmlParam != null)
            {
                XmlNode valNode = root.SelectSingleNode(xmlParam);
                if (valNode != null)
                {
                    XmlNode childNode = valNode.FirstChild;
                    if (childNode != null)
                    {
                        int.TryParse(childNode.Value, out retval);
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Reads, finds, and parses a time span value based on the name of the element it is in.
        /// </summary>
        /// <param name="root">The root element to start searching from.</param>
        /// <param name="xmlParam">The element name to look for.</param>
        /// <returns>The parsed value, or a value of 00:00:00 if not found.</returns>
        private TimeSpan ReadXmlTimeSpan(XmlNode root, string xmlParam)
        {
            TimeSpan retval = new TimeSpan();

            if (root != null && xmlParam != null)
            {
                XmlNode xmlParamNode = root.SelectSingleNode(xmlParam);
                if (xmlParamNode != null)
                {
                    XmlNode childNode = xmlParamNode.FirstChild;
                    if (childNode != null)
                    {
                        TimeSpan.TryParse(childNode.Value, out retval);
                    }
                }
            }

            return retval;
        }
        #endregion XML File Parsing
    }
}
