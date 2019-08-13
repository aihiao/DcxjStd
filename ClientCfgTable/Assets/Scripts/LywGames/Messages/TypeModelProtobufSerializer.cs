using System;
using System.IO;
using ProtoBuf.Meta;

namespace LywGames.Messages
{
    public class TypeModelProtobufSerializer : IProtobufSerializer
    {
        private TypeModel typeModel = null;

        public TypeModelProtobufSerializer(TypeModel typeModel)
        {
            this.typeModel = typeModel;
        }

        public void Serialize<T>(Stream dest, T value)
        {
            typeModel.Serialize(dest, value);
        }

        public object Deserialize(Stream source, Type type)
        {
            return typeModel.Deserialize(source, null, type);
        }

    }
}
