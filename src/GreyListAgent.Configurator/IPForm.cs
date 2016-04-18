using GreyListAgent.Configurator.Models;
using GreyListAgent.Configurator.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreyListAgent.Configurator
{
    public partial class IPForm : Form
    {
        public IPEntry Entry
        {
            get
            {
                int? cidr = null;
                if (edtMask.Text != "...")
                    cidr = NetworkHelper.Decimal2Cidr(edtMask.Text);
                return new IPEntry(edtIP.Text, cidr);
            }
        }

        public IPForm(IPEntry entry = null)
        {
            InitializeComponent();
            if (entry != null)
            {
                edtIP.Text = entry.IpAddress;
                if (entry.Cidr != null)
                    edtMask.Text = NetworkHelper.Cidr2Decimal((int)entry.Cidr);
            }
        }
    }
}
