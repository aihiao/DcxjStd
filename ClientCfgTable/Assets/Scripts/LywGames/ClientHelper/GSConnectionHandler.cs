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
            req = message;
        }

        public override void handleConnectionActive(IConnection connection, SocketError result)
        {
            if (result == SocketError.Success)
            {
                LoggerManager.Instance.Debug("connection {0} active then send CG_LoginGameMessage", connection.Remote);
                connection.Send(req, 1);
            }
            else
            {
                req.CallBackId = -1;
            }
        }

        public override void handleConnectionInactive(IConnection connection, SocketError result)
        {
            req.CallBackId = -1;
        }

        public override void handleRequestTimeout(IConnection connection, int userData)
        {
            throw new NotImplementedException();
        }

    }
}
