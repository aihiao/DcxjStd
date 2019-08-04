using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "LoginReq")]
    public class LoginReq : IExtensible
    {
        [ProtoContract(Name = "LocalLoginReq")]
        public class LocalLoginReq : IExtensible
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
        [ProtoContract(Name = "PlatformLoginReq")]
        public class PlatformLoginReq : IExtensible
        {
            private string _email = "";
            private string _password = "";
            private string _randomSeed = "";
            private string _version = "";
            private int _channelID = 0;
            private DeviceInfoPro _deviceInfo = null;
            private string _userId = "";
            private string _channelCode = "";
            private string _token = "";
            private string _productCode = "";
            private string _channelUserId = "";
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
            [ProtoMember(1), DefaultValue("")]
            public string userId
            {
                get
                {
                    return this._userId;
                }
                set
                {
                    this._userId = value;
                }
            }
            [ProtoMember(1), DefaultValue("")]
            public string channelCode
            {
                get
                {
                    return this._channelCode;
                }
                set
                {
                    this._channelCode = value;
                }
            }
            [ProtoMember(1), DefaultValue("")]
            public string token
            {
                get
                {
                    return this._token;
                }
                set
                {
                    this._token = value;
                }
            }
            [ProtoMember(1), DefaultValue("")]
            public string productCode
            {
                get
                {
                    return this._productCode;
                }
                set
                {
                    this._productCode = value;
                }
            }
            [ProtoMember(1), DefaultValue("")]
            public string channelUserId
            {
                get
                {
                    return this._channelUserId;
                }
                set
                {
                    this._channelUserId = value;
                }
            }
            IExtension IExtensible.GetExtensionObject(bool createIfMissing)
            {
                return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
            }
        }
        private LoginReq.LocalLoginReq _localLoginReq = null;
        private LoginReq.PlatformLoginReq _platformLoginReq = null;
        private IExtension extensionObject;
        [ProtoMember(1), DefaultValue(null)]
        public LoginReq.LocalLoginReq localLoginReq
        {
            get
            {
                return this._localLoginReq;
            }
            set
            {
                this._localLoginReq = value;
            }
        }
        [ProtoMember(1), DefaultValue(null)]
        public LoginReq.PlatformLoginReq platformLoginReq
        {
            get
            {
                return this._platformLoginReq;
            }
            set
            {
                this._platformLoginReq = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
