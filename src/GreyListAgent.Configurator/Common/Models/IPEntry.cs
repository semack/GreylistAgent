using NetTools;
using System;
using System.Linq;
using System.Net;

namespace GreyListAgent.Configurator.Models
{
    public class IPEntry
    {
        public IPAddress IpAddress { get; private set; }
        public int? Cidr { get; private set; }

        public static IPEntry Parse(string source)
        {
            IPAddress ip = null;
            int? cidr = null;
            string[] separators = new string[] { "/" };
            string[] items = source.Trim().Split(separators, StringSplitOptions.None);

            if (items.Count() > 0)
            {
                ip = IPAddress.Parse(items[0]);
                if (items.Count() > 1)
                {
                   cidr = int.Parse(items[1]);
                }
            }
            else
                throw new ArgumentException();

            return new IPEntry(ip, cidr);
        }

        public IPEntry(IPAddress ipAddress, int? cidr = null)
        {
            IpAddress = ipAddress;

            if (cidr != null)
            {
                var value = ToString(IpAddress, cidr);
                IPAddressRange.Parse(value);
            }
            
            Cidr = cidr;
        }

        private string ToString(IPAddress ipAddress, int? cidr = null)
        {
            if (cidr != null)
                return string.Format("{0}/{1}", ipAddress, cidr);
            return ipAddress.ToString();
        }

        public new string ToString()
        {
            return ToString(IpAddress, Cidr);
        }
    }
}
