using System;
using LywGames.Common;

namespace LywGames.Messages
{
    public abstract class Message
    {
        public static byte UNCOMPRESSED = 0;
        public static byte COMPRESSED = 1;
        private int callback;

        public int ProtocolId
        {
            get
            {
                return GetProtocolId(base.GetType());
            }
        }

        public int Callback
        {
            get
            {
                return this.callback;
            }
            set
            {
                this.callback = value;
            }
        }

        public virtual int Result
        {
            get
            {
                return 0;
            }
        }

        public NetworkBuffer EncodeWithSnappyProtocolIDCallback()
        {
            NetworkBuffer networkBuffer = new NetworkBuffer(NetworkParameters._MAX_SEND_BUFFER_SIZE, true);
            networkBuffer.Write(UNCOMPRESSED);
            networkBuffer.Write(this.ProtocolId);
            networkBuffer.Write(this.callback);
            EncodeBody(networkBuffer);

            return networkBuffer;
        }

        public virtual Message DecodeBody(byte[] buffer, int offset, int count)
        {
            return this;
        }

        public virtual void EncodeBody(NetworkBuffer outbuffer)
        {
        }

        public static int GetProtocolId(Type messageType)
        {
            object[] customAttributes = messageType.GetCustomAttributes(typeof(MessageAttribute), false);
            if (customAttributes.Length != 0)
            {
                return (customAttributes[0] as MessageAttribute).ProtocolId;
            }

            throw new Exception("MessageAttribute in " + messageType);
        }

    }
}
