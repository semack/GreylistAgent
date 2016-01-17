using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyListAgent.Configurator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (!Directory.Exists(Constants.RelativeDataPath))
            //    Directory.CreateDirectory(Constants.RelativeDataPath);
            //var filename = Path.Combine(Constants.RelativeDataPath, Constants.ConfigFileName + ".1");
            //var settings = GreyListSettings.Load(filename);
            //settings.WhitelistIPs.Add("123.0.0.1");
            //settings.WhitelistIPs.Add("123.0.0.4"); settings.WhitelistIPs.Add("123.0.0.3");

            //settings.WhitelistClients.Add("elkeldldjd");
            //settings.WhitelistClients.Add("elkeldldjd");
            //settings.WhitelistClients.Add("elkeldldjd");
            //settings.Save(filename);
        }
    }
}
