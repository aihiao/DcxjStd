namespace LywGames.Network
{
    public class MessageOutProcessor : AbstractNetworkOutHandler
    {
        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            base.sendBuffDown(connection, buffer, offset, size);
        }
        public override void Send(IConnection connection, object msg)
        {
            base.sendObjectDown(connection, msg);
        }
    }
}
