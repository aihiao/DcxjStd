using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "CreateAccountReq")]
    public class CreateAccountReq : IExtensible
    {
        private string _email = "";
        private string _password = "";
        private string _randomSeed = "";
        private string _version = "";
        private int _channelID = 0;
        private DeviceInfoPro _deviceInfo = null;
        private IExtension extensionObject;
        [ProtoMember(1), DefaultValue("")]
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
        [ProtoMember(1), DefaultValue("")]
        public string password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }
        [ProtoMember(1), DefaultValue("")]
        public string randomSeed
        {
            get
            {
                return this._randomSeed;
            }
            set
            {
                this._randomSeed = value;
            }
        }
        [ProtoMember(1), DefaultValue("")]
        public string version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }
        [ProtoMember(1), DefaultValue(0)]
        public int channelID
        {
            get
            {
                return this._channelID;
            }
            set
            {
                this._channelID = value;
            }
        }
        [ProtoMember(1), DefaultValue(null)]
        public DeviceInfoPro deviceInfo
        {
            get
            {
                return this._deviceInfo;
            }
            set
            {
                this._deviceInfo = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
