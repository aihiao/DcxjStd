using System;
using System.Collections.Generic;
using ProtoBuf;
using ProtoBuf.Meta;

namespace LywGames.Protocol
{
    public class Protocols_c : TypeModel
    {
        protected override object Deserialize(int key, object value, ProtoReader source)
        {
            throw new NotImplementedException();
        }

        protected override int GetKeyImpl(Type type)
        {
            throw new NotImplementedException();
        }

        protected override void Serialize(int key, object value, ProtoWriter dest)
        {
            throw new NotImplementedException();
        }
    }
}
