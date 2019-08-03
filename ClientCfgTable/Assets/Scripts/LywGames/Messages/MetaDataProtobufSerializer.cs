using System;
using System.IO;
using ProtoBuf;

namespace LywGames.Messages
{
    public class MetaDataProtobufSerializer : IProtobufSerializer
    {
        public void Serialize<T>(Stream dest, T value)
        {
            Serializer.Serialize<T>(dest, value);
        }

        public object Deserialize(Stream source, Type type)
        {
            return Serializer.NonGeneric.Deserialize(type, source);
        }
    }
}
