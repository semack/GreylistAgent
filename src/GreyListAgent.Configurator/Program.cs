using System;
using System.Linq;
using System.Windows.Forms;
using GreyListAgent.Configurator.Common;
using GreyListAgent.Configurator.Forms;

namespace GreyListAgent.Configurator
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Form form = null;
            var asArgv = Environment.GetCommandLineArgs();

            if (asArgv.Contains("-register"))
                ControlPanelControl.Register();
            else if (asArgv.Contains("-unregister"))
                ControlPanelControl.Unregister();
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