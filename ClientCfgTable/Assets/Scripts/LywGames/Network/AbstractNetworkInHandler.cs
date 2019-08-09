using System.Net.Sockets;

namespace LywGames.Network
{
    public abstract class AbstractNetworkInHandler
    {
        private AbstractNetworkInHandler nextInHandler;
        public AbstractNetworkInHandler NextInHandler
        {
            get
            {
                return nextInHandler;
            }
            set
            {
                nextInHandler = value;
            }
        }

        public bool FireConnected(IConnection connection, SocketError result)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnConnected(connection, result);
                return true;
            }

            return false;
        }

        public virtual void OnConnected(IConnection connection, SocketError result)
        {
            FireConnected(connection, result);
        }

        public bool FireRequestTimeout(IConnection connection, int userData)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnRequestTimeout(connection, userData);
                return true;
            }
            
            return false;
        }

        public virtual void OnRequestTimeout(IConnection connection, int userData)
        {
            FireRequestTimeout(connection, userData);
        }

        public abstract void OnReceived(IConnection connection, object msg);
        public abstract void OnReceived(IConnection connection, byte[] buffer, int offset, int size);

        public bool FireBuffReceived(IConnection connection, byte[] buffer, int offset, int size)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnReceived(connection, buffer, offset, size);
                return true;
            }
            
            return false;
        }

        public bool FireObjectReceived(IConnection connection, object msg)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnReceived(connection, msg);
                return true;
            }
            
            return false;
        }

        public bool FireDisconnected(IConnection connection, SocketError error)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnDisconnected(connection, error);
                return true;
            }
            
            return false;
        }

        public virtual void OnDisconnected(IConnection connection, SocketError error)
        {
            FireDisconnected(connection, error);
        }

    }
}
