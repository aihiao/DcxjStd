using System.Net.Sockets;

namespace LywGames.Network
{
    public abstract class AbstractNetworkInHandler
    {
        private AbstractNetworkInHandler nextInHandler;
        protected object obj;
        public AbstractNetworkInHandler NextInHandler
        {
            get
            {
                return this.nextInHandler;
            }
            set
            {
                this.nextInHandler = value;
            }
        }
        public abstract void OnReceived(IConnection connection, byte[] buffer, int offset, int size);
        public abstract void OnReceived(IConnection connection, object msg);
        public bool FireBuffReceived(IConnection connection, byte[] buffer, int offset, int size)
        {
            bool result;
            if (this.NextInHandler != null)
            {
                this.NextInHandler.OnReceived(connection, buffer, offset, size);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool FireObjectReceived(IConnection connection, object msg)
        {
            bool result;
            if (this.NextInHandler != null)
            {
                this.NextInHandler.OnReceived(connection, msg);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool FireConnected(IConnection connection, SocketError result)
        {
            bool result2;
            if (this.NextInHandler != null)
            {
                this.NextInHandler.OnConnected(connection, result);
                result2 = true;
            }
            else
            {
                result2 = false;
            }
            return result2;
        }
        public bool FireDisconnected(IConnection connection, SocketError error)
        {
            bool result;
            if (this.NextInHandler != null)
            {
                this.NextInHandler.OnDisconnected(connection, error);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool FireRequestTimeout(IConnection connection, int userData)
        {
            bool result;
            if (this.NextInHandler != null)
            {
                this.NextInHandler.OnRequestTimeout(connection, userData);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public virtual void OnConnected(IConnection connection, SocketError result)
        {
            this.FireConnected(connection, result);
        }
        public virtual void OnDisconnected(IConnection connection, SocketError error)
        {
            this.FireDisconnected(connection, error);
        }
        public virtual void OnRequestTimeout(IConnection connection, int userData)
        {
            this.FireRequestTimeout(connection, userData);
        }
    }
}
