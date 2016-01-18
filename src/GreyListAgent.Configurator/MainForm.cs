using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GreyListAgent.Configurator
{
    public partial class MainForm : Form
    {
        private readonly GreyListSettings settings;

        public MainForm()
        {
            settings = GreyListSettings.Load(Path.Combine(Constants.RelativeDataPath, Constants.ConfigFileName));
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Constants.RelativeDataPath))
                Directory.CreateDirectory(Constants.RelativeDataPath);
            var filename = Path.Combine(Constants.RelativeDataPath, Constants.ConfigFileName + ".1");
            var settings = GreyListSettings.Load(filename);
            //settings.WhitelistIPs.Add("123.0.0.1");
            //settings.WhitelistIPs.Add("123.0.0.4"); settings.WhitelistIPs.Add("123.0.0.3");

            //settings.WhitelistClients.Add("elkeldldjd");
            //settings.WhitelistClients.Add("elkeldldjd");
            //settings.WhitelistClients.Add("elkeldldjd");
            settings.Save(filename);
        }

        private void lblHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Constants.HomePageUri);
        }

        private void lblAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("mailto:{0}", Constants.AuthorEmail));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //tpConfirmedMaxAge.Value = settings.ConfirmedMaxAge;
        }
    }
}
