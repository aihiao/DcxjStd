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

        public override void HandleConnectionActive(IConnection connection, SocketError result)
        {
            if (result == SocketError.Success)
            {
                LoggerManager.Instance.Debug("AsConnection {0} Active then send message", connection.Remote.Address);
                connection.Send(req, 1);
            }
            else
            {
                clientHelper.OnASConnectFailed();
            }
        }

        public override void HandleConnectionInactive(IConnection connection, SocketError result)
        {
            clientHelper.OnASConnectFailed();
        }

        public override void HandleRequestTimeout(IConnection connection, int userData)
        {
            throw new NotImplementedException();
        }
    }
}
