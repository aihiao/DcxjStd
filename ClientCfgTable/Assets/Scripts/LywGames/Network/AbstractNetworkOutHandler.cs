using System;

namespace LywGames.Network
{
    public abstract class AbstractNetworkOutHandler
    {
        protected object obj;
        private AbstractNetworkOutHandler nextOutHandler;
        public AbstractNetworkOutHandler NextOutHandler
        {
            get
            {
                return this.nextOutHandler;
            }
            set
            {
                this.nextOutHandler = value;
            }
        }

        protected void sendBuffDown(IConnection connection, byte[] buffer, int offset, int size)
        {
            if (this.NextOutHandler != null)
            {
                this.NextOutHandler.Send(connection, buffer, offset, size);
            }
            else
            {
                connection._Send(buffer, offset, size);
            }
        }

        protected void sendObjectDown(IConnection connection, object msg)
        {
            if (this.NextOutHandler != null)
            {
                this.NextOutHandler.Send(connection, msg);
                return;
            }
            throw new NotSupportedException("The last ConnectionOutHandler can't be invoked Send(IConnection connection, Object msg)");
        }

        public abstract void Send(IConnection connection, byte[] buffer, int offset, int size);
        public abstract void Send(IConnection connection, object msg);
    }
}
