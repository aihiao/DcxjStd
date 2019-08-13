using System;
using LywGames.Common;

namespace LywGames.Messages
{
    public class ProtocolMessage <T> : Message where T : new()
    {
        protected T protocol = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
        public T Protocol
        {
            get
            {
                return protocol;
            }
        }

        public override Message DecodeBody(byte[] buffer, int offset, int count)
        {
            protocol = (T)MySerializer.GetInstance().DeserializeByteBuffer(buffer, offset, count, typeof(T));
            return this;
        }

        public override void EncodeBody(NetworkBuffer outbuffer)
        {
            MySerializer.GetInstance().Serialize<T>(outbuffer, protocol);
        }

    }
}
