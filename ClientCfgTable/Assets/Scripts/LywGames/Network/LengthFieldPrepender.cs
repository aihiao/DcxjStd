using System;
using LywGames.Common;

namespace LywGames.Network
{
    public class LengthFieldPrepender : AbstractNetworkOutHandler
    {
        private int lengthFieldLength;
        private int lengthAdjustment;
        private bool lengthIncludesLengthFieldLength;

        public LengthFieldPrepender(int lengthFieldLength, int lengthAdjustment, bool lengthIncludesLengthFieldLength)
        {
            if (lengthFieldLength != 1 && lengthFieldLength != 2 && lengthFieldLength != 4)
            {
                throw new ArgumentException("maxFrameLength must be either 1, 2, 4: " + lengthFieldLength);
            }
            this.lengthFieldLength = lengthFieldLength;
            this.lengthAdjustment = lengthAdjustment;
            this.lengthIncludesLengthFieldLength = lengthIncludesLengthFieldLength;
        }

        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            int num = size + lengthAdjustment;
            if (lengthIncludesLengthFieldLength)
            {
                num += lengthFieldLength;
            }
            if (num < 0)
            {
                throw new ArgumentException("Adjusted frame length (" + num + ") is less than zero");
            }

            NetworkBuffer networkBuffer = new NetworkBuffer(size + lengthFieldLength, true);
            switch (lengthFieldLength)
            {
                case 1:
                    if (num >= 256)
                    {
                        throw new ArgumentException("length does not fit into a byte: " + num);
                    }
                    networkBuffer.Write((byte)num);
                    goto IL_EC;
                case 2:
                    if (num >= 65536)
                    {
                        throw new ArgumentException("length does not fit into a short integer: " + num);
                    }
                    networkBuffer.Write((short)num);
                    goto IL_EC;
                case 4:
                    networkBuffer.Write(num);
                    goto IL_EC;
            }
            throw new ArgumentException("should not reach here");
            IL_EC:

            networkBuffer.Write(buffer, offset, size);
            SendBuffDown(connection, networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
        }

        public override void Send(IConnection connection, object msg)
        {
            throw new NotSupportedException("LengthFieldPrepender didn't implement Send(IConnection connection, Object msg)");
        }

    }
}
