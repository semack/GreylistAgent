using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GreyListAgent.Configurator.Common.Helpers;
using GreyListAgent.Configurator.Common.Models;
using log4net;
using log4net.Config;

namespace GreyListAgent.Configurator.Forms
{
    public partial class MainForm : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(Constants.ConfiguratorId);
        private readonly GreyListSettings _settings;

        private bool _hasChanges;

        public MainForm()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(ConfigPath, Constants.LoggerConfigFileName)));
            _settings = GreyListSettings.Load(ConfigFileName);
            InitializeComponent();
        }

        private string ConfigPath
        {
            get
            {
                var currAssembly = Assembly.GetAssembly(GetType());
                var assemblyPath = Path.GetDirectoryName(currAssembly.Location);
                return Path.Combine(assemblyPath, Constants.RelativeConfigPath);
            }
        }

        private string ConfigFileName
        {
            get
            {
                if (!Directory.Exists(ConfigPath))
                    Directory.CreateDirectory(ConfigPath);

                return Path.Combine(ConfigPath, Constants.AgentConfigFileName);
            }
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
            Process.Start($"mailto:{Constants.AuthorEmail}");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            _hasChanges = false;

            tcMain.SelectedIndex = 0;

            if (lbClientList.Items.Count > 0)
                lbClientList.SelectedItem = lbClientList.Items[0];

            if (lbIPList.Items.Count > 0)
                lbIPList.SelectedItem = lbIPList.Items[0];
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
            Log.Info("Configuration loaded");
        }

        private void SaveSettings()
        {
            _settings.CleanRowCount = (int) edtCleanRowCount.Value;
            _settings.GreylistingPeriod = edtGreyListPeriod.ValueTimeSpan;
            _settings.ConfirmedMaxAge = edtMaxAgeConfirmed.ValueTimeSpan;
            _settings.UnconfirmedMaxAge = edtMaxAgeUnConfirmed.ValueTimeSpan;
            _settings.IpNetmask = NetworkHelper.Decimal2Cidr(edtNetmask.Text);
            _settings.WhitelistClients.Clear();
            _settings.WhitelistIPs.Clear();

            foreach (var item in lbClientList.Items)
            {
                _settings.WhitelistClients.Add((string) item);
            }

            foreach (var item in lbIPList.Items)
            {
                _settings.WhitelistIPs.Add((string) item);
            }
            _settings.Save(ConfigFileName);
            Log.Info("Configuration has been saved");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_hasChanges)
            {
                SaveSettings();
                MessageBox.Show(@"To apply changes you need to restart Exchange Transport Agent", @"Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            _hasChanges = true;
        }

        private void alMain_Update(object sender, EventArgs e)
        {
            aEditClient.Enabled = lbClientList.SelectedItem != null;
            aRemoveClient.Enabled = lbClientList.SelectedItem != null;
            aAddIP.Enabled = lbIPList.SelectedItem != null;
            aRemoveIP.Enabled = lbIPList.SelectedItem != null;
        }

        private void aAddIP_Execute(object sender, EventArgs e)
        {
            using (var form = new IpEnterForm())
            {
                form.Text = @"Add";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var item = form.Entry.ToString();
                    lbIPList.Items.Add(item);
                    lbIPList.SelectedItem = item;
                    _hasChanges = true;
                }
            }
        }

        private void aEditIP_Execute(object sender, EventArgs e)
        {
            var index = lbIPList.SelectedIndex;
            var item = lbIPList.SelectedItem as string;
            var args = IpEntry.Parse(item);
            using (var form = new IpEnterForm(args))
            {
                form.Text = @"Update";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    item = form.Entry.ToString();
                    lbIPList.Items.RemoveAt(index);
                    lbIPList.Items.Insert(index, item);
                    lbIPList.SelectedIndex = index;
                    _hasChanges = true;
                }
            }
        }

        private void aRemoveIP_Execute(object sender, EventArgs e)
        {
            lbIPList.Items.RemoveAt(lbIPList.SelectedIndex);
            _hasChanges = true;
        }
    }
}