namespace GreyListAgent
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class GreyListSettings
    {
        /// <summary>
        /// The maximum age of an confirmed entry.
        /// </summary>
        private TimeSpan confirmedMaxAge;

        /// <summary>
        /// The maximum age of an unconfirmed entry.
        /// </summary>
        private TimeSpan unconfirmedMaxAge;

        /// <summary>
        /// Period of minimum time for a greylisting
        /// </summary>
        private TimeSpan greylistingPeriod;

        /// <summary>
        /// The number of rows to be cleaned each pass
        /// </summary>
        private int cleanRowCount;

        /// <summary>
        /// An empty constructor initializes with default values.
        /// </summary>
        /// <param name="path">The path to an XML file that contains the settings.</param>
        public GreyListSettings(string path)
        {
            // 30 days
            this.confirmedMaxAge = new TimeSpan(30, 0, 0, 0);

            // 4 hours
            this.unconfirmedMaxAge = new TimeSpan(0, 4, 0, 0);

            // 5 minutes.
            this.greylistingPeriod = new TimeSpan(0, 0, 5, 0);

            // Default cleaning row count.
            this.CleanRowCount = 100;

            // Read nondefault settings from file.
            this.ReadXMLConfig(path);
        }

        public GreyListSettings(GreyListSettings other)
        {
            this.ConfirmedMaxAge = other.ConfirmedMaxAge;
            this.UnconfirmedMaxAge = other.UnconfirmedMaxAge;
            this.GreylistingPeriod = other.GreylistingPeriod;
            this.CleanRowCount = other.CleanRowCount;
        }

        public TimeSpan ConfirmedMaxAge
        {
            get { return this.confirmedMaxAge; }

            set { this.confirmedMaxAge = value; }
        }

        public TimeSpan UnconfirmedMaxAge
        {
            get { return this.unconfirmedMaxAge; }

            set { this.unconfirmedMaxAge = value; }
        }

        public TimeSpan GreylistingPeriod
        {
            get { return this.greylistingPeriod; }

            set { this.greylistingPeriod = value; }
        }

       
        public int CleanRowCount
        {
            get { return this.cleanRowCount; }

            set { this.cleanRowCount = value; }
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
