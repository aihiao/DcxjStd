using System.Net.Sockets;
using LywGames.Messages;

namespace LywGames.Network
{
    public abstract class AbstractConnectionHandler : IMessageHandler
    {
        public virtual void HandleMessage(IConnection connection, Message message)
        {
        }

        public virtual void HandleConnectionActive(IConnection connection, SocketError result)
        {
        }

        public abstract void HandleConnectionInactive(IConnection connection, SocketError result);
        public abstract void HandleRequestTimeout(IConnection connection, int userData);
    }
}
