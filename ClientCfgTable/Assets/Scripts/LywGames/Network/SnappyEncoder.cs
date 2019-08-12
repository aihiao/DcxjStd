using LywGames.Messages;
using LywGames.Common;

namespace LywGames.Network
{
    public class SnappyEncoder : AbstractNetworkOutHandler
    {
        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            NetworkBuffer networkBuffer = new NetworkBuffer(size + 1, true);
            networkBuffer.Write(Message.UnCompressed);
            networkBuffer.Write(buffer, offset, size);
            SendBuffDown(connection, networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
        }

        public override void Send(IConnection connection, object msg)
        {
            if (msg is Message)
            {
                Message message = (Message)msg;
                NetworkBuffer networkBuffer = message.EncodeWithSnappyProtocolIdCallBackId();
                SendBuffDown(connection, networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
            }
        }

    }
}
