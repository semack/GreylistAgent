using System;
using System.Linq;
using System.Net;
using NetTools;

namespace GreyListAgent.Configurator.Common.Models
{
    public class IpEntry
    {
        public IpEntry(IPAddress ipAddress, int? cidr = null)
        {
            IpAddress = ipAddress;

            if (cidr != null)
            {
                var value = ToString(IpAddress, cidr);
                IPAddressRange.Parse(value);
            }

            Cidr = cidr;
        }

        public IPAddress IpAddress { get; }
        public int? Cidr { get; }

        public static IpEntry Parse(string source)
        {
            IPAddress ip;
            int? cidr = null;
            string[] separators = {"/"};
            var items = source.Trim().Split(separators, StringSplitOptions.None);

            if (items.Any())
            {
                ip = IPAddress.Parse(items[0]);
                if (items.Length > 1)
                {
                    cidr = int.Parse(items[1]);
                }
            }
            else
                throw new ArgumentException();

            return new IpEntry(ip, cidr);
        }

        private string ToString(IPAddress ipAddress, int? cidr = null)
        {
            if (cidr != null)
                return $"{ipAddress}/{cidr}";
            return ipAddress.ToString();
        }

        public new string ToString()
        {
            return ToString(IpAddress, Cidr);
        }
    }
}