using GreyListAgent.Configurator.Models;
using GreyListAgent.Configurator.Utils;
using System;
using System.Windows.Forms;

namespace GreyListAgent.Configurator
{
    public partial class IPForm : Form
    {
        private IPEntry ParseInput()
        {
            int? cidr = null;
            if (edtMask.Text != "...")
                cidr = NetworkHelper.Decimal2Cidr(edtMask.IPAddress.ToString());
            return new IPEntry(edtIP.IPAddress, cidr);
        }

        public IPEntry Entry
        {
            get
            {
                return ParseInput();
            }
        }

        public IPForm(IPEntry entry = null)
        {
            InitializeComponent();
            if (entry != null)
            {
                edtIP.IPAddress = entry.IpAddress;
                if (entry.Cidr != null)
                    edtMask.Text = NetworkHelper.Cidr2Decimal((int)entry.Cidr);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ParseInput(); // Validate input
                Close();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
