using System.Net.Sockets;

namespace LywGames.Network
{
    public class NetworkManager : AbstractNetworkManager
    {
        private static NetworkManager instance = new NetworkManager();
        public static AbstractNetworkManager Instance
        {
            get
            {
                return instance;
            }
        }
        public static AbstractNetworkManager GetInstance()
        {
            return instance;
        }

        public override IConnection CreateConnection(ProtocolType type, int timeout)
        {
            if (type == ProtocolType.Tcp)
            {
                connection = new TCPConnection(timeout);
            }
            else if (type == ProtocolType.Udp)
            {   
                connection = new UdpAdpaterConnection(timeout); 
            }

            return connection;
        }

        public override IConnection GetConnection()
        {
            return connection;
        }

    }
}
