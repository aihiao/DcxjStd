using System;
using System.Net;
using System.Net.Sockets;

namespace LywGames.Network
{
    public class NetUtil
    {
        public static IPAddress GetIPV4Address(string hostName)
        {
            IPAddress[] ipAddressArray = null;
            try
            {
                ipAddressArray = Dns.GetHostAddresses(hostName);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error("Dns.GetHostAddresses exception {0}", ex.Message);
                return null;
            }

            if (ipAddressArray != null && ipAddressArray.Length > 0)
            {
                for (int i = 0; i < ipAddressArray.Length; i++)
                {
                    IPAddress ipAddress = ipAddressArray[i];
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipAddress;
                    }
                }
            }
            else
            {
                LoggerManager.Instance.Error("Dns.GetHostAddresses null");
            }

            return null;
        }

        public static long GetLongAddress(byte[] array)
        {
            uint num1 = array[0];
            uint num2 = array[1];
            uint num3 = array[2];
            uint num4 = array[3];
            return (((num4 * 256u + num3) * 256u + num2) * 256u + num1);
        }

    }
}
