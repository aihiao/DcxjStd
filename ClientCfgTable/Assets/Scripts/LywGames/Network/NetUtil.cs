using System;
using System.Net;
using System.Net.Sockets;

namespace LywGames.Network
{
    public class NetUtil
    {
        public static IPAddress GetIPV4Address(string hostname)
        {
            IPAddress[] array = null;
            IPAddress result;
            try
            {
                array = Dns.GetHostAddresses(hostname);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error("Dns.GetHostAddresses exception " + ex.Message, new object[0]);
                result = null;
                return result;
            }
            if (array == null || array.Length == 0)
            {
                LoggerManager.Instance.Error("Dns.GetHostAddresses null ", new object[0]);
                result = null;
            }
            else
            {
                IPAddress iPAddress = null;
                IPAddress[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    IPAddress iPAddress2 = array2[i];
                    if (iPAddress2.AddressFamily == AddressFamily.InterNetwork)
                    {
                        iPAddress = iPAddress2;
                        break;
                    }
                }
                result = iPAddress;
            }
            return result;
        }

        private static IPAddress GetIPAddrFromString(string ipStr)
        {
            string[] array = ipStr.Split(new char[]
            {
                '.'
            });
            uint num = uint.Parse(array[0]);
            uint num2 = uint.Parse(array[1]);
            uint num3 = uint.Parse(array[2]);
            uint num4 = uint.Parse(array[3]);
            ulong newAddress = (ulong)(((num4 * 256u + num3) * 256u + num2) * 256u + num);
            return new IPAddress((long)newAddress);
        }

    }
}
