using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "CreateAccountRes")]
    public class CreateAccountRes : IExtensible
    {
        private int _result;
        private IExtension extensionObject;
        [ProtoMember(1)]
        public int result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
