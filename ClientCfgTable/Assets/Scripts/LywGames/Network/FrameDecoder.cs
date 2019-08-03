using System;
using LywGames.Messages;
using LywGames.Common;

namespace LywGames.Network
{
    public abstract class FrameDecoder : AbstractNetworkInHandler
    {
        protected NetworkBuffer msgBuffer;
        public override void OnReceived(IConnection connection, byte[] buffer, int offset, int count)
        {
            if (this.msgBuffer == null)
            {
                this.msgBuffer = new NetworkBuffer(NetworkParameters._MAX_COMPRESS_MESSAGE_SIZE, true);
            }
            this.msgBuffer.Write(buffer, offset, count);
            try
            {
                while (this.msgBuffer.Readable)
                {
                    int readOffset = this.msgBuffer.ReadOffset;
                    int num = 0;
                    if (!this.Decode(this.msgBuffer, ref num))
                    {
                        if (readOffset == this.msgBuffer.ReadOffset)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (num == 0)
                        {
                            throw new InvalidOperationException("decode() method must read at least one byte if it returned a frame ");
                        }
                        try
                        {
                            base.FireBuffReceived(connection, this.msgBuffer.GetBuffer(), this.msgBuffer.ReadOffset, num);
                        }
                        finally
                        {
                            this.msgBuffer.ReadOffset = this.msgBuffer.ReadOffset + num;
                            this.msgBuffer.DiscardReadBytes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Info("OnReceived connection {0} catch Exception {1}", new object[]
                {
                    connection.ToString(),
                    ex.ToString()
                });
            }
        }
        public override void OnReceived(IConnection connection, object msg)
        {
            throw new NotSupportedException("FrameDecoder didn't implement OnReceived(IConnection connection, Object msg)");
        }
        public abstract bool Decode(NetworkBuffer msgBuffer, ref int msgCount);
    }
}
