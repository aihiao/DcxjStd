namespace LywGames.Network
{
    public class MessageOutProcessor : AbstractNetworkOutHandler
    {
        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            SendBuffDown(connection, buffer, offset, size);
        }
        public override void Send(IConnection connection, object msg)
        {
            SendObjectDown(connection, msg);
        }
    }
}
