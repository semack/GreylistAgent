using System;
using System.Windows.Forms;
using GreyListAgent.Configurator.Common.Helpers;
using GreyListAgent.Configurator.Common.Models;

namespace GreyListAgent.Configurator.Forms
{
    public partial class IpEnterForm : Form
    {
        public IpEnterForm(IpEntry entry = null)
        {
            InitializeComponent();
            if (entry != null)
            {
                edtIP.IPAddress = entry.IpAddress;
                if (entry.Cidr != null)
                    edtMask.Text = NetworkHelper.Cidr2Decimal((int) entry.Cidr);
            }
        }

        public IpEntry Entry => ParseInput();

        private IpEntry ParseInput()
        {
            int? cidr = null;
            if (edtMask.Text != @"...")
                cidr = NetworkHelper.Decimal2Cidr(edtMask.IPAddress.ToString());
            return new IpEntry(edtIP.IPAddress, cidr);
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
                MessageBox.Show(ex.Message, @"Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}