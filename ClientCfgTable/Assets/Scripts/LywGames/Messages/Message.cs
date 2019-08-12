using System;
using LywGames.Common;

namespace LywGames.Messages
{
    public abstract class Message
    {
        public static byte UnCompressed = 0;
        public static byte Compressed = 1;

        public int ProtocolId
        {
            get
            {
                return GetProtocolId(base.GetType());
            }
        }

        private int callBackId;
        public int CallBackId
        {
            get
            {
                return callBackId;
            }
            set
            {
                callBackId = value;
            }
        }

        public virtual int ResultCode
        {
            get
            {
                return 0;
            }
        }

        public NetworkBuffer EncodeWithSnappyProtocolIdCallBackId()
        {
            NetworkBuffer networkBuffer = new NetworkBuffer(NetworkParameters.MaxSendBufferSize, true);
            networkBuffer.Write(UnCompressed);
            networkBuffer.Write(ProtocolId);
            networkBuffer.Write(callBackId);
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
