using System;

namespace LywGames.Network
{
    public abstract class AbstractNetworkOutHandler
    {
        private AbstractNetworkOutHandler nextOutHandler;
        public AbstractNetworkOutHandler NextOutHandler
        {
            get
            {
                return nextOutHandler;
            }
            set
            {
                nextOutHandler = value;
            }
        }

        protected void SendBuffDown(IConnection connection, byte[] buffer, int offset, int size)
        {
            if (NextOutHandler != null)
            {
                NextOutHandler.Send(connection, buffer, offset, size);
            }
            else
            {
                connection.Send(buffer, offset, size);
            }
        }

        protected void SendObjectDown(IConnection connection, object msg)
        {
            if (NextOutHandler != null)
            {
                NextOutHandler.Send(connection, msg);
                return;
            }

            throw new NotSupportedException("The last ConnectionOutHandler can't be invoked Send(IConnection connection, Object msg)");
        }

        public abstract void Send(IConnection connection, byte[] buffer, int offset, int size);
        public abstract void Send(IConnection connection, object msg);
    }
}
