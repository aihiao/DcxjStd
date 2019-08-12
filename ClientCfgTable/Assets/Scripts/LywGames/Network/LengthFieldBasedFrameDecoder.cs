using System;
using LywGames.Common;

namespace LywGames.Network
{
    public class LengthFieldBasedFrameDecoder : FrameDecoder
    {
        private int maxFrameLength; // 最大的长度, 防止太大导致内存溢出
        private int lengthFieldOffset; // 表示消息体长度的字段的偏移, 也就是Buffer的什么位置开始就是长度字段了
        private int lengthFieldLength; // 长度字段的长度
        private int lengthFieldEndOffset;
        private int lengthAdjustment; // 有些情况可能会把消息头也包含到长度字段中, 或者长度字段后面还有一些不包括在长度字段内的, 可以通过lengthAdjustment调节
        private int initialBytesToStrip; // 起始截掉的部分, 如果传递给后面的Handler的数据不需要消息头了, 可以通过这个设置
        private long tooLongFrameLength;
        private long bytesToDiscard;

        public LengthFieldBasedFrameDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip)
        {
            if (maxFrameLength <= 0)
            {
                throw new ArgumentException("maxFrameLength must be a positive integer: " + maxFrameLength);
            }
            if (lengthFieldOffset < 0)
            {
                throw new ArgumentException("lengthFieldOffset must be a non-negative integer: " + lengthFieldOffset);
            }
            if (initialBytesToStrip < 0)
            {
                throw new ArgumentException("initialBytesToStrip must be a non-negative integer: " + initialBytesToStrip);
            }
            if (lengthFieldLength != 1 && lengthFieldLength != 2 && lengthFieldLength != 4)
            {
                throw new ArgumentException("lengthFieldLength must be either 1, 2, or 4: " + lengthFieldLength);
            }
            if (lengthFieldOffset > maxFrameLength - lengthFieldLength)
            {
                throw new ArgumentException(string.Concat(new object[]
                {
                    "maxFrameLength (",
                    maxFrameLength,
                    ") must be equal to or greater than lengthFieldOffset (",
                    lengthFieldOffset,
                    ") + lengthFieldLength (",
                    lengthFieldLength,
                    ")."
                }));
            }

            this.maxFrameLength = maxFrameLength;
            this.lengthFieldOffset = lengthFieldOffset;
            this.lengthFieldLength = lengthFieldLength;
            this.lengthAdjustment = lengthAdjustment;
            this.initialBytesToStrip = initialBytesToStrip;

            lengthFieldEndOffset = lengthFieldOffset + lengthFieldLength;
        }

        public override bool Decode(NetworkBuffer msgBuffer, ref int msgCount)
        {
            bool result;
            if (msgBuffer.ReadableBytes < lengthFieldEndOffset)
            {
                result = false;
            }
            else
            {
                int offset = msgBuffer.ReadOffset + lengthFieldOffset;
                int num;
                switch (lengthFieldLength)
                {
                    case 1:
                        num = msgBuffer.GetByte(offset);
                        goto IL_7A;
                    case 2:
                        num = msgBuffer.GetUInt16(offset);
                        goto IL_7A;
                    case 4:
                        num = msgBuffer.GetInt32(offset);
                        goto IL_7A;
                }
                throw new Exception("should not reach here");
                IL_7A:
                if (num < 0)
                {
                    msgBuffer.SkipBytes(lengthFieldEndOffset);
                    throw new Exception("negative pre-adjustment length field: " + num);
                }
                num += lengthAdjustment + lengthFieldEndOffset;
                if (num < lengthFieldEndOffset)
                {
                    msgBuffer.SkipBytes(lengthFieldEndOffset);
                    throw new Exception(string.Concat(new object[]
                    {
                        "Adjusted frame length (",
                        num,
                        ") is less than lengthFieldEndOffset: ",
                        lengthFieldEndOffset
                    }));
                }
                if (num > maxFrameLength)
                {
                    tooLongFrameLength = (long)num;
                    bytesToDiscard = (long)(num - msgBuffer.ReadableBytes);
                    msgBuffer.SkipBytes(msgBuffer.ReadableBytes);
                    throw new Exception(string.Format("too long frame, frame length:{0}", tooLongFrameLength));
                }
                int num2 = num;
                if (msgBuffer.ReadableBytes < num2)
                {
                    LoggerManager.Instance.Info("msgBuffer.ReadableBytes {0} < frameLengthInt {1}", msgBuffer.ReadableBytes, num2);
                    result = false;
                }
                else
                {
                    if (initialBytesToStrip > num2)
                    {
                        msgBuffer.SkipBytes(num2);
                        throw new Exception(string.Concat(new object[]
                        {
                            "Adjusted frame length (",
                            num,
                            ") is less than initialBytesToStrip: ",
                            initialBytesToStrip
                        }));
                    }
                    msgBuffer.SkipBytes(initialBytesToStrip);
                    msgCount = num2 - initialBytesToStrip;
                    result = true;
                }
            }

            return result;
        }

    }
}
