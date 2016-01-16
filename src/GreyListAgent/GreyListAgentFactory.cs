namespace GreyListAgent
{
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;
    using log4net.Config;
    using log4net;
    public class GreyListAgentFactory : SmtpReceiveAgentFactory
    {
        /// <summary>
        /// Directory for storing the data relative to the DLL
        /// </summary>
        private const string RelativeDataPath = @"GreyListAgentData\";

        /// <summary>
        /// Configuration filename for GreyList Configuration
        /// </summary>
        private const string ConfigFileName = "GreyListAgent.config";

        /// <summary>
        /// Configuration filename for Log4Net Configuration
        /// </summary>
        private const string LoggerConfigFileName = "GreyListAgent.Log4Net.config";

        /// <summary>
        /// Database filename for persistant storage of the database
        /// </summary>
        private const string DatabaseFile = "GreyListDatabase.xml";

        /// <summary>
        /// Crypto hash manager
        /// </summary>
        private SHA256Managed hashManager = new SHA256Managed();

        /// <summary>
        /// GreyList Settings
        /// </summary>
        private GreyListSettings greylistSettings;

        /// <summary>
        /// GreyList Database
        /// </summary>
        private GreyListDatabase greylistDatabase;

        /// <summary>
        /// Will contain the absolute path for RelativeDataPath
        /// </summary>
        private string dataPath;

        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GreyListAgentFactory()
        {
            // Initialize the hashing engine
            hashManager = new SHA256Managed();

            // Fetch the assembly, and populate paths
            Assembly currAssembly = Assembly.GetAssembly(this.GetType());
            string assemblyPath = Path.GetDirectoryName(currAssembly.Location);
            this.dataPath = Path.Combine(assemblyPath, RelativeDataPath);
            
            // Configuring Log4Net
            XmlConfigurator.Configure(new FileInfo(Path.Combine(this.dataPath, LoggerConfigFileName)));

            // Load GreyList settings from file
            this.greylistSettings = new GreyListSettings(Path.Combine(this.dataPath, ConfigFileName));

            // Load the database. The database will end up empty if the file doesn't exist or becomes corrupted
            this.greylistDatabase = GreyListDatabase.Load(Path.Combine(this.dataPath, DatabaseFile));
        }

        /// <summary>
        /// Saves the GreyList Database on close
        /// </summary>
        public override void Close()
        {
            this.greylistDatabase.Save(Path.Combine(this.dataPath, DatabaseFile));
        }

        /// <summary>
        /// Create a new GreyList Agent.
        /// </summary>
        /// <param name="server">Exchange Edge Transport server.</param>
        /// <returns>A new Transport Agent.</returns>
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            return new GreyListAgent(
                                     this.greylistSettings,
                                     this.greylistDatabase,
                                     this.hashManager,
                                     server,
                                     log);
        }
    }
}
