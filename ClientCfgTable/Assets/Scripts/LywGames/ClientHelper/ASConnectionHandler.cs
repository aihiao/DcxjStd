using System;
using System.Net.Sockets;
using LywGames.Messages;
using LywGames.Network;

namespace LywGames.ClientHelper
{
    public class ASConnectionHandler : AbstractConnectionHandler
    {
        private Message req;
        private ClientHelper clientHelper;

        public ASConnectionHandler(Message req, ClientHelper clientHelper)
        {
            this.req = req;
            this.clientHelper = clientHelper;
        }

        public override void handleConnectionActive(IConnection connection, SocketError result)
        {
            if (result == SocketError.Success)
            {
                LoggerManager.Instance.Debug("AsConnection {0} Active then send message", new object[]
                {
                    connection.Remote.Address
                });
                connection.Send(this.req, 1);
            }
            else
            {
                this.clientHelper.OnAsConnectFailed();
            }
        }

        public override void handleConnectionInactive(IConnection connection, SocketError result)
        {
            this.clientHelper.OnAsConnectFailed();
        }

        public override void handleRequestTimeout(IConnection connection, int userData)
        {
            throw new NotImplementedException();
        }
    }
}
