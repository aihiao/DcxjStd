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
            if (msgBuffer == null)
            {
                msgBuffer = new NetworkBuffer(NetworkParameters.MaxCompressMessageSize, true);
            }
            msgBuffer.Write(buffer, offset, count);

            try
            {
                while (msgBuffer.Readable)
                {
                    int readOffset = msgBuffer.ReadOffset;
                    int num = 0;
                    if (!Decode(msgBuffer, ref num))
                    {
                        if (readOffset == msgBuffer.ReadOffset)
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
                            FireBuffReceived(connection, msgBuffer.GetBuffer(), msgBuffer.ReadOffset, num);
                        }
                        finally
                        {
                            msgBuffer.ReadOffset = msgBuffer.ReadOffset + num;
                            msgBuffer.DiscardReadBytes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Info("OnReceived connection {0} catch Exception {1}", connection.ToString(), ex.ToString());
            }
        }

        public override void OnReceived(IConnection connection, object msg)
        {
            throw new NotSupportedException("FrameDecoder didn't implement OnReceived(IConnection connection, Object msg)");
        }

        public abstract bool Decode(NetworkBuffer msgBuffer, ref int msgCount);
    }
}
