using System;
using System.Net.Sockets;
using LywGames.Messages;
using LywGames.Network;

namespace LywGames.ClientHelper
{
    public class GSConnectionHandler : AbstractConnectionHandler
    {
        private CGLoginGameMessage req;

        public GSConnectionHandler(CGLoginGameMessage message)
        {
            this.req = message;
        }

        public override void handleConnectionActive(IConnection connection, SocketError result)
        {
            if (result == SocketError.Success)
            {
                LoggerManager.Instance.Debug("connection {0} active then send CG_LoginGameMessage", new object[]
                {
                    connection.Remote
                });
                connection.Send(this.req, 1);
            }
            else
            {
                this.req.Callback = -1;
            }
        }

        public override void handleConnectionInactive(IConnection connection, SocketError result)
        {
            this.req.Callback = -1;
        }

        public override void handleRequestTimeout(IConnection connection, int userData)
        {
            throw new NotImplementedException();
        }

    }
}
