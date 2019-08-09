using System.Net.Sockets;

namespace LywGames.Network
{
    public abstract class AbstractNetworkManager
    {
        protected IConnection connection;

        public abstract IConnection CreateConnection(ProtocolType type, int userData);
        public abstract IConnection GetConnection();
    }
}
