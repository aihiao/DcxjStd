using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "ActiveCodeReq")]
    public class ActiveCodeReq : IExtensible
    {
        private long _accountId = 0L;
        private string _activeCode = "";
        private IExtension extensionObject;
        [ProtoMember(1), DefaultValue(0L)]
        public long accountId
        {
            get
            {
                return this._accountId;
            }
            set
            {
                this._accountId = value;
            }
        }
        [ProtoMember(1), DefaultValue("")]
        public string activeCode
        {
            get
            {
                return this._activeCode;
            }
            set
            {
                this._activeCode = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
