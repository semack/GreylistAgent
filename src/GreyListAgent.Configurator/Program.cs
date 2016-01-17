﻿using GreyListAgent.Configurator.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyListAgent.Configurator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form form = null;
            string[] asArgv = Environment.GetCommandLineArgs();

            if (asArgv.Contains("-register"))
                CplControl.Register();
            else if (asArgv.Contains("-unregister"))
                CplControl.Unregister();
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                form = new MainForm();
            }

            if (form != null)
                Application.Run(form);
        }
    }
}
