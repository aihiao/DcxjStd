using System.Net.Sockets;
using LywGames.Messages;

namespace LywGames.Network
{
    public interface IMessageHandler
    {
        void HandleMessage(IConnection connection, Message message);
        void HandleConnectionActive(IConnection connection, SocketError result);
        void HandleConnectionInactive(IConnection connection, SocketError result);
        void HandleRequestTimeout(IConnection connection, int userData);
    }
}
