using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "ChangePasswordReq")]
    public class ChangePasswordReq : IExtensible
    {
        private string _email;
        private string _newPassword;
        private string _oldPassword;
        private IExtension extensionObject;
        [ProtoMember(1)]
        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }
        [ProtoMember(1)]
        public string newPassword
        {
            get
            {
                return this._newPassword;
            }
            set
            {
                this._newPassword = value;
            }
        }
        [ProtoMember(1)]
        public string oldPassword
        {
            get
            {
                return this._oldPassword;
            }
            set
            {
                this._oldPassword = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
