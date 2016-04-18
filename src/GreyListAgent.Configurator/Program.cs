using GreyListAgent.Configurator.Configuration;
using System;
using System.Linq;
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
