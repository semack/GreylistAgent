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
        private readonly GreyListSettings _settings;

        private bool _hasChanges;

        private string ConfigFileName {
            get {
                if (!Directory.Exists(Constants.RelativeConfigPath))
                    Directory.CreateDirectory(Constants.RelativeConfigPath);

                return Path.Combine(Constants.RelativeConfigPath, Constants.AgentConfigFileName);
            }
        }

        public MainForm()
        {
            _settings = GreyListSettings.Load(ConfigFileName);
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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
            _hasChanges = false;
        }

        private void LoadSettings()
        {
            edtCleanRowCount.Value = _settings.CleanRowCount;
            edtGreyListPeriod.ValueTimeSpan = _settings.GreylistingPeriod;
            edtMaxAgeConfirmed.ValueTimeSpan = _settings.ConfirmedMaxAge;
            edtMaxAgeUnConfirmed.ValueTimeSpan = _settings.UnconfirmedMaxAge;
            edtNetmask.Text = NetworkHelper.Cidr2Decimal(_settings.IpNetmask);

            lbClientList.Items.Clear();
            lbIPList.Items.Clear();

            _settings.WhitelistClients.ForEach(item => { lbClientList.Items.Add(item); });
            _settings.WhitelistIPs.ForEach(item => { lbIPList.Items.Add(item); });
        }

        private void SaveSettings()
        {
            _settings.CleanRowCount = (int)edtCleanRowCount.Value;
            _settings.GreylistingPeriod = edtGreyListPeriod.ValueTimeSpan;
            _settings.ConfirmedMaxAge = edtMaxAgeConfirmed.ValueTimeSpan;
            _settings.UnconfirmedMaxAge = edtMaxAgeUnConfirmed.ValueTimeSpan;
            _settings.IpNetmask = NetworkHelper.Decimal2Cidr(edtNetmask.Text);
            _settings.WhitelistClients.Clear();
            _settings.WhitelistIPs.Clear();

            foreach (var item in lbClientList.Items)
            {
                _settings.WhitelistClients.Add((string)item);
            }

            foreach (var item in lbIPList.Items)
            {
                _settings.WhitelistIPs.Add((string)item);
            }
            _settings.Save(ConfigFileName);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_hasChanges)
            {
                SaveSettings();
                MessageBox.Show("To apply changes you need to restart Exchange Transport Agent", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            _hasChanges = true;
        }
    }
}
