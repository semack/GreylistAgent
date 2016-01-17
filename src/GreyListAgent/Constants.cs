using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreyListAgent
{
    public static class Constants
    {
        /// <summary>
        /// Directory for storing the data relative to the DLL
        /// </summary>
        public const string RelativeDataPath = @"GreyListAgentData\";

        /// <summary>
        /// Configuration filename for GreyList Configuration
        /// </summary>
        public const string ConfigFileName = "GreyListAgent.config";

        /// <summary>
        /// Configuration filename for Log4Net Configuration
        /// </summary>
        public const string LoggerConfigFileName = "GreyListAgent.Log4Net.config";

        /// <summary>
        /// Database filename for persistant storage of the database
        /// </summary>
        public const string DatabaseFile = "GreyListDatabase.xml";
    }
}
