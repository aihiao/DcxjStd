using System.Net.Sockets;
using LywGames.Messages;

namespace LywGames.Network
{
    public abstract class AbstractConnectionHandler : IMessageHandler
    {
        public virtual void handleMessage(IConnection connection, Message message)
        {
        }

        public virtual void handleConnectionActive(IConnection connection, SocketError result)
        {
        }

        public abstract void handleConnectionInactive(IConnection connection, SocketError result);
        public abstract void handleRequestTimeout(IConnection connection, int userData);
    }
}
