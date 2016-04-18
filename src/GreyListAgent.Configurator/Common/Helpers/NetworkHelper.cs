using System;

namespace GreyListAgent.Configurator.Utils
{
    public static class NetworkHelper
    {
        public static string Cidr2Decimal(int cidr)
        {
            string[] decim = new string[4];

            // We go through each octagon in the decimal address
            for (int i = 0; i < 4; i++)
            {
                if (cidr > 8)
                {
                    decim[i] = "255";
                    cidr -= 8;
                }
                else
                {
                    int temp = 0;
                    for (int a = 7; cidr > 0; a--, cidr--)
                    {
                        temp += (int)Math.Pow(2, a);
                    }
                    decim[i] = temp.ToString();
                }
            }
            return decim[0] + "." + decim[1] + "." + decim[2] + "." + decim[3];
        }

        public static int Decimal2Cidr(string decim)
        {
            int cidr = 0;
            string[] proof = decim.Split('.');
            bool lessthan255 = false;

            // We expect the string to be in the form of z.x.y.w , if there are more or less than 3 dots in there then something is wrong
            if (proof.Length != 4)
            {
                throw new Exception("Input must be in the form z.x.y.w");
            }

            // Loop through every octed to process it's number
            foreach (string oct in proof)
            {
                // We need to convert the string to an integer. We use a TryParse and see if the input is really a number and it is between 0 and 255
                int noct;

                if (!Int32.TryParse(oct, out noct) || noct > 255 || noct < 0)
                {
                    throw new Exception(oct + " is not a valid octet.");
                }

                if (noct == 255)
                {
                    // Let's check if we are supposed to have this here.
                    if (lessthan255)
                    {
                        throw new Exception("This is not a valid netmask.");
                    }

                    // If the octet is 255 then we already know we are working with an 8.
                    cidr += 8;
                }
                else
                {
                    // We need to process the octet and calculate the CIDR

                    // First we check if we've already had an octet less than 255 which would indicate that there is no need for processing
                    // If there are still numbers here other than 0 then this is an invalid mask and we throw out a generic exception
                    if (lessthan255 && oct != "0")
                    {
                        throw new Exception("This is not a valid netmask.");
                    }

                    // If we are working with something that is less than 255 then we shouldn't have to process further octets
                    // but we do anyway to validate that the rest of the netmask is in a correct format. We need to know that
                    // we have reached this point so we have this boolean here.
                    lessthan255 = true;

                    int temp = 0;
                    for (int a = 7; temp != noct; a--, cidr++)
                    {
                        // If we have already gone 8 rounds here and not gotten our desired value then the octet is invalid and we throw an error
                        if (a < 0)
                        {
                            throw new Exception("This is not a valid netmask.");
                        }
                        temp += (int)Math.Pow(2, a);
                    }
                }
            }

            // We should now have calculated the CIDR value so we return it.
            return cidr;
        }
    }
}