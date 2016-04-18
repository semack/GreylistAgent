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
            this.dataPath = Path.Combine(assemblyPath, Constants.RelativeConfigPath);
            
            // Configuring Log4Net
            XmlConfigurator.Configure(new FileInfo(Path.Combine(this.dataPath, Constants.LoggerConfigFileName)));

            // Load GreyList settings from file
            this.greylistSettings = GreyListSettings.Load(Path.Combine(this.dataPath, Constants.AgentConfigFileName));

            // Load the database. The database will end up empty if the file doesn't exist or becomes corrupted
            this.greylistDatabase = GreyListDatabase.Load(Path.Combine(this.dataPath, Constants.DatabaseFile));
        }

        /// <summary>
        /// Saves the GreyList Database on close
        /// </summary>
        public override void Close()
        {
            if (!Directory.Exists(Constants.RelativeConfigPath))
                Directory.CreateDirectory(Constants.RelativeConfigPath);

            this.greylistDatabase.Save(Path.Combine(this.dataPath, Constants.DatabaseFile));
        }

        /// <summary>
        /// Create a new GreyList Agent.
        /// </summary>
        /// <param name="server">Exchange Edge Transport server.</param>
        /// <returns>A new Transport Agent.</returns>
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(this.dataPath, Constants.LoggerConfigFileName)));
            return new GreyListAgent(
                                     this.greylistSettings,
                                     this.greylistDatabase,
                                     this.hashManager,
                                     server,
                                     log);
        }
    }
}
