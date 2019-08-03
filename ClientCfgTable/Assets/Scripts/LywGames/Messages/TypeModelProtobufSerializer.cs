using System;
using System.IO;
using ProtoBuf.Meta;

namespace LywGames.Messages
{
    public class TypeModelProtobufSerializer : IProtobufSerializer
    {
        private TypeModel typeModelSerializer = null;
        public TypeModelProtobufSerializer(TypeModel typeMode)
        {
            this.typeModelSerializer = typeMode;
        }

        public void Serialize<T>(Stream dest, T value)
        {
            typeModelSerializer.Serialize(dest, value);
        }

        public object Deserialize(Stream source, Type type)
        {
            return typeModelSerializer.Deserialize(source, null, type);
        }

    }
}
