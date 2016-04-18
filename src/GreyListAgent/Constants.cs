using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreyListAgent
{
    public static class Constants
    {
        public const string AgentId = "Agent";

        public const string ConfiguratorId = "Configurator";
        /// <summary>
        /// Directory for storing the data relative to the DLL
        /// </summary>
        public const string RelativeConfigPath = @"Configs\";

        /// <summary>
        /// Configuration filename for GreyList Configuration
        /// </summary>
        public const string AgentConfigFileName = "Agent.config";

        /// <summary>
        /// Configuration filename for Log4Net Configuration
        /// </summary>
        public const string LoggerConfigFileName = "Log.config";

        /// <summary>
        /// Database filename for persistant storage of the database
        /// </summary>
        public const string DatabaseFile = "GreyListDatabase.xml";

        public const string HomePageUri = "https://github.com/jmdevince/GreylistAgent";

        public const string AuthorEmail = "james@hexhost.net";
    }
}
