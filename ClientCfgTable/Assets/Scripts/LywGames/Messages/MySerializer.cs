using System;
using System.IO;
using LywGames.Common;
using LywGames.Protocol;

namespace LywGames.Messages
{
    public class MySerializer
    {
        public static int MAX_OUTMESSAGE_SIZE = 2048;
        private IProtobufSerializer protobufSerializer;

        private static MySerializer mySerializer = new MySerializer();
        public static MySerializer GetInstance()
        {
            return mySerializer;
        }
        private MySerializer()
        {
        }

        public void Initialize(bool useTypeMode)
        {
            if (useTypeMode)
            {
                protobufSerializer = new TypeModelProtobufSerializer(new Protocols_c());
            }
            else
            {
                protobufSerializer = new MetaDataProtobufSerializer();
            }
        }

        public bool Serialize<T>(NetworkBuffer outBuffer, T instance)
        {
            outBuffer.GetStream().Position = (long)outBuffer.WriteOffset;
            protobufSerializer.Serialize<T>(outBuffer.GetStream(), instance);
            outBuffer.WriteOffset = (int)outBuffer.GetStream().Position;

            return true;
        }

        public object DeserializeByteBuffer(byte[] buffer, int offset, int size, Type protoType)
        {
            MemoryStream source = new MemoryStream(buffer, offset, size);
            object result = null;
            try
            {
                result = protobufSerializer.Deserialize(source, protoType);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.StackTrace);
            }

            return result;
        }

    }
}
