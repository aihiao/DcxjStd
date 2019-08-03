using System;
using System.IO;
using LywGames.Common;
using LywGames.Protocol;

namespace LywGames.Messages
{
    public class MySerializer
    {
        private IProtobufSerializer protobufSerializer;

        private static MySerializer mySerializer = new MySerializer();
        public static int MAX_OUTMESSAGE_SIZE = 2048;
        private MySerializer()
        {
        }
        public static MySerializer GetInstance()
        {
            return mySerializer;
        }

        public void Initialize(bool useTypeMode)
        {
            if (useTypeMode)
            {
                this.protobufSerializer = new TypeModelProtobufSerializer(new Protocols_c());
            }
            else
            {
                this.protobufSerializer = new MetaDataProtobufSerializer();
            }
        }

        public bool Serialize<T>(NetworkBuffer outBuffer, T instance)
        {
            outBuffer.GetStream().Position = (long)outBuffer.WriteOffset;
            this.protobufSerializer.Serialize<T>(outBuffer.GetStream(), instance);
            outBuffer.WriteOffset = (int)outBuffer.GetStream().Position;
            return true;
        }

        public object DeserializeByteBuffer(byte[] buffer, int offset, int size, Type protoType)
        {
            MemoryStream source = new MemoryStream(buffer, offset, size);
            object result = null;
            try
            {
                result = this.protobufSerializer.Deserialize(source, protoType);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.StackTrace);
            }
            return result;
        }

    }
}
