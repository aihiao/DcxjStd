using System.Net.Sockets;

namespace LywGames.Network
{
    public class NetworkManager : INetworkManager
    {
        private static NetworkManager instance = new NetworkManager();
        public static INetworkManager Instance
        {
            get
            {
                return instance;
            }
        }
        public static INetworkManager GetInstance()
        {
            return instance;
        }

        public IConnection CreateConnection(ProtocolType type, int timeout)
        {
            IConnection result;
            if (type == ProtocolType.Tcp)
            {
                result = new TCPConnection(timeout);
            }
            else
            {
                if (type == ProtocolType.Udp)
                {
                    result = new UdpAdpaterConnection(timeout);
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public IConnection SearchConnectoin(int connectionID)
        {
            return null;
        }

        public bool RemoveConnection(int connectionID)
        {
            return true;
        }
    }
}
