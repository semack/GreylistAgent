using NetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace GreyListAgent.Configurator.Models
{
    public class IPEntry
    {
        public string IpAddress { get; private set; }
        public int? Cidr { get; private set; }

        public static IPEntry Parse(string source)
        {
            string ip = null;
            int? cidr = null;
            string[] separators = new string[] { "/" };
            string[] items = source.Trim().Split(separators, StringSplitOptions.None);

            if (items.Count() > 0)
            {
                ip = items[0];
                IPAddress.Parse(ip);
                if (items.Count() > 1)
                {
                   cidr = int.Parse(items[1]);
                }
            }
            else
                throw new ArgumentException();

            return new IPEntry(ip, cidr);
        }

        public IPEntry(string ipAddress, int? cidr = null)
        {
            IPAddress.Parse(ipAddress);

            if (cidr != null)
            {
                var value = ToString(ipAddress, cidr);
                IPAddressRange.Parse(value);
            }
            IpAddress = ipAddress;
            Cidr = cidr;
        }

        private string ToString(string ipAddress, int? cidr = null)
        {
            if (cidr != null)
                return string.Format("{0}/{1}", ipAddress, cidr);
            return ipAddress;
        }

        public new string ToString()
        {
            return ToString(IpAddress, Cidr);
        }
    }
}
