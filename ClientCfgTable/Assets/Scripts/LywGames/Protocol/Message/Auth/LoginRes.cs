using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace LywGames.Message.Proto.Auth
{
    [ProtoContract(Name = "LoginRes")]
    public class LoginRes : IExtensible
    {
        [ProtoContract(Name = "ChannelMessage")]
        public class ChannelMessage : IExtensible
        {
            private string _accessToken = "";
            private string _channelUniqueId = "";
            private string _channelUserName = "";
            private string _oid = "";
            private IExtension extensionObject;
            [ProtoMember(1), DefaultValue("")]
            public string accessToken
            {
                get
                {
                    return this._accessToken;
                }
                set
                {
                    this._accessToken = value;
                }
            }
            [ProtoMember(2), DefaultValue("")]
            public string channelUniqueId
            {
                get
                {
                    return this._channelUniqueId;
                }
                set
                {
                    this._channelUniqueId = value;
                }
            }
            [ProtoMember(1), DefaultValue("")]
            public string channelUserName
            {
                get
                {
                    return this._channelUserName;
                }
                set
                {
                    this._channelUserName = value;
                }
            }
            [ProtoMember(1), DefaultValue("")]
            public string oid
            {
                get
                {
                    return this._oid;
                }
                set
                {
                    this._oid = value;
                }
            }
            IExtension IExtensible.GetExtensionObject(bool createIfMissing)
            {
                return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
            }
        }
        [ProtoContract(Name = "AreaPro")]
        public class AreaPro : IExtensible
        {
            private int _areaID;
            private string _name;
            private int _status;
            private string _interfaceServerIP;
            private int _interfaceServerPort;
            private int _areaAvatarNumber = 0;
            private int _showAreaID = 0;
            private bool _isNewServer;
            private bool _isRecommendServer;
            private IExtension extensionObject;
            [ProtoMember(1)]
            public int areaID
            {
                get
                {
                    return this._areaID;
                }
                set
                {
                    this._areaID = value;
                }
            }
            [ProtoMember(1)]
            public string name
            {
                get
                {
                    return this._name;
                }
                set
                {
                    this._name = value;
                }
            }
            [ProtoMember(1)]
            public int status
            {
                get
                {
                    return this._status;
                }
                set
                {
                    this._status = value;
                }
            }
            [ProtoMember(1)]
            public string interfaceServerIP
            {
                get
                {
                    return this._interfaceServerIP;
                }
                set
                {
                    this._interfaceServerIP = value;
                }
            }
            [ProtoMember(1)]
            public int interfaceServerPort
            {
                get
                {
                    return this._interfaceServerPort;
                }
                set
                {
                    this._interfaceServerPort = value;
                }
            }
            [ProtoMember(1), DefaultValue(0)]
            public int areaAvatarNumber
            {
                get
                {
                    return this._areaAvatarNumber;
                }
                set
                {
                    this._areaAvatarNumber = value;
                }
            }
            [ProtoMember(1), DefaultValue(0)]
            public int showAreaID
            {
                get
                {
                    return this._showAreaID;
                }
                set
                {
                    this._showAreaID = value;
                }
            }
            [ProtoMember(1)]
            public bool isNewServer
            {
                get
                {
                    return this._isNewServer;
                }
                set
                {
                    this._isNewServer = value;
                }
            }
            [ProtoMember(1)]
            public bool isRecommendServer
            {
                get
                {
                    return this._isRecommendServer;
                }
                set
                {
                    this._isRecommendServer = value;
                }
            }
            IExtension IExtensible.GetExtensionObject(bool createIfMissing)
            {
                return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
            }
        }
        private int _result;
        private long _accountID = 0L;
        private string _token = "";
        private readonly List<LoginRes.AreaPro> _areas = new List<LoginRes.AreaPro>();
        private int _lastAreaID = -1;
        private bool _isFirstQuickLogin = false;
        private bool _isShowActiveInterface = false;
        private LoginRes.ChannelMessage _channel = null;
        private long _forbidEndTime = 0L;
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
        [ProtoMember(1), DefaultValue(0L)]
        public long accountID
        {
            get
            {
                return this._accountID;
            }
            set
            {
                this._accountID = value;
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
        [ProtoMember(1)]
        public List<LoginRes.AreaPro> areas
        {
            get
            {
                return this._areas;
            }
        }
        [ProtoMember(1), DefaultValue(-1)]
        public int lastAreaID
        {
            get
            {
                return this._lastAreaID;
            }
            set
            {
                this._lastAreaID = value;
            }
        }
        [ProtoMember(1), DefaultValue(false)]
        public bool isFirstQuickLogin
        {
            get
            {
                return this._isFirstQuickLogin;
            }
            set
            {
                this._isFirstQuickLogin = value;
            }
        }
        [ProtoMember(1), DefaultValue(false)]
        public bool isShowActiveInterface
        {
            get
            {
                return this._isShowActiveInterface;
            }
            set
            {
                this._isShowActiveInterface = value;
            }
        }
        [ProtoMember(1), DefaultValue(null)]
        public LoginRes.ChannelMessage channel
        {
            get
            {
                return this._channel;
            }
            set
            {
                this._channel = value;
            }
        }
        [ProtoMember(1), DefaultValue(0L)]
        public long forbidEndTime
        {
            get
            {
                return this._forbidEndTime;
            }
            set
            {
                this._forbidEndTime = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}

