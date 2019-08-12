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
            if (b == Message.UnCompressed)
            {
                FireBuffReceived(connection, buffer, offset + 1, size - 1);
            }
            else
            {
                if (b == Message.Compressed)
                {
                    byte[] array = decompressor.Decompress(buffer, offset + 1, size - 1);
                    FireBuffReceived(connection, array, 0, array.Length);
                }
            }
        }

        public override void OnReceived(IConnection connection, object msg)
        {
            throw new NotSupportedException("SnappyFramedDecoder didn't implement OnReceived(IConnection connection, Object msg)");
        }

    }
}
