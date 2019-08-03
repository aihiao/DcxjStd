using System;
using Snappy.Sharp;
using LywGames.Messages;

namespace LywGames.Network
{
    public class SnappyDecoder : AbstractNetworkInHandler
    {
        private SnappyDecompressor decompressor = new SnappyDecompressor();
        public override void OnReceived(IConnection connection, byte[] buffer, int offset, int size)
        {
            byte b = buffer[offset];
            if (b == Message.UNCOMPRESSED)
            {
                base.FireBuffReceived(connection, buffer, offset + 1, size - 1);
            }
            else
            {
                if (b == Message.COMPRESSED)
                {
                    byte[] array = this.decompressor.Decompress(buffer, offset + 1, size - 1);
                    base.FireBuffReceived(connection, array, 0, array.Length);
                }
            }
        }
        public override void OnReceived(IConnection connection, object msg)
        {
            throw new NotSupportedException("SnappyFramedDecoder didn't implement OnReceived(IConnection connection, Object msg)");
        }
    }
}
