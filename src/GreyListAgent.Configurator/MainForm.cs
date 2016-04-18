using GreyListAgent.Configurator.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace GreyListAgent.Configurator
{
    public partial class MainForm : Form
    {
        private readonly GreyListSettings settings;

        public MainForm()
        {
            settings = GreyListSettings.Load(Path.Combine(Constants.RelativeConfigPath, Constants.AgentConfigFileName));
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Constants.RelativeConfigPath))
                Directory.CreateDirectory(Constants.RelativeConfigPath);
            var filename = Path.Combine(Constants.RelativeConfigPath, Constants.AgentConfigFileName + ".1");
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
            tcMain.SelectedIndex = 0;
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (!Directory.Exists(Constants.RelativeConfigPath))
                Directory.CreateDirectory(Constants.RelativeConfigPath);
            var fileName = Path.Combine(Constants.RelativeConfigPath, Constants.AgentConfigFileName);
            var settings = GreyListSettings.Load(fileName);

            edtCleanRowCount.Value = settings.CleanRowCount;
            edtGreyListPeriod.ValueTimeSpan = settings.GreylistingPeriod;
            edtMaxAgeConfirmed.ValueTimeSpan = settings.ConfirmedMaxAge;
            edtMaxAgeUnConfirmed.ValueTimeSpan = settings.UnconfirmedMaxAge;
            edtNetmask.Text = NetworkHelper.Cidr2Decimal(settings.IpNetmask);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("To apply chnages you need to restart Exchange Transport Agent", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Close();
        }

        private void SaveSettings()
        {
            //
        }
    }
}
