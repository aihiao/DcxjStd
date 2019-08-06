using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "CreateAccountRes")]
    public class CreateAccountRes : IExtensible
    {
        private int resultCode;
        private IExtension extensionObject;

        [ProtoMember(1)]
        public int ResultCode
        {
            get
            {
                return resultCode;
            }
            set
            {
                resultCode = value;
            }
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
        }

    }
}
