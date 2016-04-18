using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using log4net;
using log4net.Config;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace GreyListAgent
{
    public class GreyListAgentFactory : SmtpReceiveAgentFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(Constants.AgentId);

        /// <summary>
        ///     Will contain the absolute path for RelativeDataPath
        /// </summary>
        private readonly string _configPath;

        /// <summary>
        ///     GreyList Database
        /// </summary>
        private readonly GreyListDatabase _greylistDatabase;

        /// <summary>
        ///     GreyList Settings
        /// </summary>
        private readonly GreyListSettings _greylistSettings;

        /// <summary>
        ///     Crypto hash manager
        /// </summary>
        private readonly SHA256Managed _hashManager;

        public GreyListAgentFactory()
        {
            // Initialize the hashing engine
            _hashManager = new SHA256Managed();

            // Fetch the assembly, and populate paths
            var currAssembly = Assembly.GetAssembly(GetType());
            var assemblyPath = Path.GetDirectoryName(currAssembly.Location);
            _configPath = Path.Combine(assemblyPath, Constants.RelativeConfigPath);

            // Configuring Log4Net
            XmlConfigurator.Configure(new FileInfo(Path.Combine(_configPath, Constants.LoggerConfigFileName)));

            // Load GreyList settings from file
            _greylistSettings = GreyListSettings.Load(Path.Combine(_configPath, Constants.AgentConfigFileName));

            // Load the database. The database will end up empty if the file doesn't exist or becomes corrupted
            _greylistDatabase = GreyListDatabase.Load(Path.Combine(_configPath, Constants.DatabaseFile));
        }

        /// <summary>
        ///     Saves the GreyList Database on close
        /// </summary>
        public override void Close()
        {
            if (!Directory.Exists(Constants.RelativeConfigPath))
                Directory.CreateDirectory(Constants.RelativeConfigPath);

            _greylistDatabase.Save(Path.Combine(_configPath, Constants.DatabaseFile));
        }

        /// <summary>
        ///     Create a new GreyList Agent.
        /// </summary>
        /// <param name="server">Exchange Edge Transport server.</param>
        /// <returns>A new Transport Agent.</returns>
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(_configPath, Constants.LoggerConfigFileName)));
            return new GreyListAgent(
                _greylistSettings,
                _greylistDatabase,
                _hashManager,
                server,
                Log);
        }
    }
}