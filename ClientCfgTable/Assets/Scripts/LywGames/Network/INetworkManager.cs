using System.Net.Sockets;

namespace LywGames.Network
{
    public interface INetworkManager
    {
        IConnection CreateConnection(ProtocolType type, int userData);
        IConnection SearchConnectoin(int connectionID);
        bool RemoveConnection(int connectionID);
    }
}
