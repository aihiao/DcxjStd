using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "ChangePasswordRes")]
    public class ChangePasswordRes : IExtensible
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
