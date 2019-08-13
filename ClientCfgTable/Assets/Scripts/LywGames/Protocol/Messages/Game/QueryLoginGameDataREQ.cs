using ProtoBuf;

namespace LywGames.Messages.Proto.Game
{
    [ProtoContract(Name = "QueryLoginGameDataREQ")]
    public class QueryLoginGameDataREQ : IExtensible
    {
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
        }

    }
}
