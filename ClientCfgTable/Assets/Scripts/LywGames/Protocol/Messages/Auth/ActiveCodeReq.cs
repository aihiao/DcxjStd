using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "ActiveCodeReq")]
    public class ActiveCodeReq : IExtensible
    {
        private long accountId = 0L;
        private string activeCode = "";
        private IExtension extensionObject;

        [ProtoMember(1), DefaultValue(0L)]
        public long AccountId
        {
            get
            {
                return accountId;
            }
            set
            {
                accountId = value;
            }
        }

        [ProtoMember(1), DefaultValue("")]
        public string ActiveCode
        {
            get
            {
                return activeCode;
            }
            set
            {
                activeCode = value;
            }
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
        }

    }
}
