using LywGames.Messages;
using LywGames.Common;

namespace LywGames.Network
{
    public class SnappyEncoder : AbstractNetworkOutHandler
    {
        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            NetworkBuffer networkBuffer = new NetworkBuffer(size + 1, true);
            networkBuffer.Write(Message.UNCOMPRESSED);
            networkBuffer.Write(buffer, offset, size);
            base.sendBuffDown(connection, networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
        }

        public override void Send(IConnection connection, object msg)
        {
            if (msg is Message)
            {
                Message message = (Message)msg;
                NetworkBuffer networkBuffer = message.EncodeWithSnappyProtocolIDCallback();
                base.sendBuffDown(connection, networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
            }
        }

    }
}
