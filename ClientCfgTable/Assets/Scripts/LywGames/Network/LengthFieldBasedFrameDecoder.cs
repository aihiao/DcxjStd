using System;
using LywGames.Common;

namespace LywGames.Network
{
    public class LengthFieldBasedFrameDecoder : FrameDecoder
    {
        private int maxFrameLength;
        private int lengthFieldOffset;
        private int lengthFieldLength;
        private int lengthFieldEndOffset;
        private int lengthAdjustment;
        private int initialBytesToStrip;
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
            this.lengthFieldEndOffset = lengthFieldOffset + lengthFieldLength;
            this.initialBytesToStrip = initialBytesToStrip;
        }
        public override bool Decode(NetworkBuffer msgBuffer, ref int msgCount)
        {
            bool result;
            if (msgBuffer.ReadableBytes < this.lengthFieldEndOffset)
            {
                result = false;
            }
            else
            {
                int offset = msgBuffer.ReadOffset + this.lengthFieldOffset;
                int num;
                switch (this.lengthFieldLength)
                {
                    case 1:
                        num = (int)msgBuffer.GetByte(offset);
                        goto IL_7A;
                    case 2:
                        num = (int)msgBuffer.GetUInt16(offset);
                        goto IL_7A;
                    case 4:
                        num = msgBuffer.GetInt32(offset);
                        goto IL_7A;
                }
                throw new Exception("should not reach here");
                IL_7A:
                if (num < 0)
                {
                    msgBuffer.SkipBytes(this.lengthFieldEndOffset);
                    throw new Exception("negative pre-adjustment length field: " + num);
                }
                num += this.lengthAdjustment + this.lengthFieldEndOffset;
                if (num < this.lengthFieldEndOffset)
                {
                    msgBuffer.SkipBytes(this.lengthFieldEndOffset);
                    throw new Exception(string.Concat(new object[]
                    {
                        "Adjusted frame length (",
                        num,
                        ") is less than lengthFieldEndOffset: ",
                        this.lengthFieldEndOffset
                    }));
                }
                if (num > this.maxFrameLength)
                {
                    this.tooLongFrameLength = (long)num;
                    this.bytesToDiscard = (long)(num - msgBuffer.ReadableBytes);
                    msgBuffer.SkipBytes(msgBuffer.ReadableBytes);
                    throw new Exception(string.Format("too long frame, frame length:{0}", this.tooLongFrameLength));
                }
                int num2 = num;
                if (msgBuffer.ReadableBytes < num2)
                {
                    LoggerManager.Instance.Info("msgBuffer.ReadableBytes {0} < frameLengthInt {1}", new object[]
                    {
                        msgBuffer.ReadableBytes,
                        num2
                    });
                    result = false;
                }
                else
                {
                    if (this.initialBytesToStrip > num2)
                    {
                        msgBuffer.SkipBytes(num2);
                        throw new Exception(string.Concat(new object[]
                        {
                            "Adjusted frame length (",
                            num,
                            ") is less than initialBytesToStrip: ",
                            this.initialBytesToStrip
                        }));
                    }
                    msgBuffer.SkipBytes(this.initialBytesToStrip);
                    msgCount = num2 - this.initialBytesToStrip;
                    result = true;
                }
            }
            return result;
        }
    }
}
