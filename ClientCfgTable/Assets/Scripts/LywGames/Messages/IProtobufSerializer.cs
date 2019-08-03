using System;
using System.IO;

namespace LywGames.Messages
{
    public interface IProtobufSerializer
    {
        void Serialize<T>(Stream dest, T value);
        object Deserialize(Stream source, Type type);
    }
}
