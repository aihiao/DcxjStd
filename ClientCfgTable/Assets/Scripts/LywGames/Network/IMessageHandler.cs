using System.Net.Sockets;
using LywGames.Messages;

namespace LywGames.Network
{
    public interface IMessageHandler
    {
        void handleMessage(IConnection connection, Message message);
        void handleConnectionActive(IConnection connection, SocketError result);
        void handleConnectionInactive(IConnection connection, SocketError result);
        void handleRequestTimeout(IConnection connection, int userData);
    }
}
