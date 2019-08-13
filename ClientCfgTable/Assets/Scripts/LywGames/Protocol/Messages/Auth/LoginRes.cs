using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "LoginRes")]
    public class LoginRes : IExtensible
    {
        // 类前加上ProtoContract Attrbuit
        [ProtoContract(Name = "ChannelMessage")]
        public class ChannelMessage : IExtensible
        {
            private string accessToken = "";
            private string channelUniqueId = "";
            private string channelUserName = "";
            private string oid = "";
            private IExtension extensionObject;

            // 成员加上ProtoMember Attribute即可, 其中ProtoMember需要一个大于0的int类型的值。原则上这个int类型没有大小限制, 但建议从1开始, 这是一个良好的习惯, 另外这个参数必需是这个类成员的唯一标识, 不可重复。
            [ProtoMember(1), DefaultValue("")]
            public string AccessToken
            {
                get
                {
                    return accessToken;
                }
                set
                {
                    accessToken = value;
                }
            }

            [ProtoMember(2), DefaultValue("")]
            public string ChannelUniqueId
            {
                get
                {
                    return channelUniqueId;
                }
                set
                {
                    channelUniqueId = value;
                }
            }

            [ProtoMember(3), DefaultValue("")]
            public string ChannelUserName
            {
                get
                {
                    return channelUserName;
                }
                set
                {
                    channelUserName = value;
                }
            }

            [ProtoMember(4), DefaultValue("")]
            public string Oid
            {
                get
                {
                    return oid;
                }
                set
                {
                    oid = value;
                }
            }

            IExtension IExtensible.GetExtensionObject(bool createIfMissing)
            {
                return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
            }

        }

        [ProtoContract(Name = "AreaPro")]
        public class AreaPro : IExtensible
        {
            private int areaId;
            private string name;
            private int status;
            private string interfaceServerIp;
            private int interfaceServerPort;
            private int areaAvatarNumber = 0;
            private int showAreaId = 0;
            private bool isNewServer;
            private bool isRecommendServer;
            private IExtension extensionObject;

            [ProtoMember(1)]
            public int AreaId
            {
                get
                {
                    return areaId;
                }
                set
                {
                    areaId = value;
                }
            }

            [ProtoMember(2)]
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            [ProtoMember(3)]
            public int Status
            {
                get
                {
                    return status;
                }
                set
                {
                    status = value;
                }
            }

            [ProtoMember(4)]
            public string InterfaceServerIp
            {
                get
                {
                    return interfaceServerIp;
                }
                set
                {
                    interfaceServerIp = value;
                }
            }

            [ProtoMember(5)]
            public int InterfaceServerPort
            {
                get
                {
                    return interfaceServerPort;
                }
                set
                {
                    interfaceServerPort = value;
                }
            }

            [ProtoMember(6), DefaultValue(0)]
            public int AreaAvatarNumber
            {
                get
                {
                    return areaAvatarNumber;
                }
                set
                {
                    areaAvatarNumber = value;
                }
            }

            [ProtoMember(7), DefaultValue(0)]
            public int ShowAreaId
            {
                get
                {
                    return showAreaId;
                }
                set
                {
                    showAreaId = value;
                }
            }

            [ProtoMember(8)]
            public bool IsNewServer
            {
                get
                {
                    return isNewServer;
                }
                set
                {
                    isNewServer = value;
                }
            }

            [ProtoMember(9)]
            public bool IsRecommendServer
            {
                get
                {
                    return isRecommendServer;
                }
                set
                {
                    isRecommendServer = value;
                }
            }

            IExtension IExtensible.GetExtensionObject(bool createIfMissing)
            {
                return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
            }

        }

        private int resultCode;
        private long accountId = 0L;
        private string token = "";
        private readonly List<AreaPro> areaList = new List<AreaPro>();
        private int lastAreaId = -1;
        private bool isFirstQuickLogin = false;
        private bool isShowActiveInterface = false;
        private ChannelMessage channelMsg = null;
        private long forbidEndTime = 0L;
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

        [ProtoMember(2), DefaultValue(0L)]
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

        [ProtoMember(3), DefaultValue("")]
        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }

        [ProtoMember(4)]
        public List<AreaPro> AreaList
        {
            get
            {
                return areaList;
            }
        }

        [ProtoMember(5), DefaultValue(-1)]
        public int LastAreaId
        {
            get
            {
                return lastAreaId;
            }
            set
            {
                lastAreaId = value;
            }
        }

        [ProtoMember(6), DefaultValue(false)]
        public bool IsFirstQuickLogin
        {
            get
            {
                return isFirstQuickLogin;
            }
            set
            {
                isFirstQuickLogin = value;
            }
        }

        [ProtoMember(7), DefaultValue(false)]
        public bool IsShowActiveInterface
        {
            get
            {
                return isShowActiveInterface;
            }
            set
            {
                isShowActiveInterface = value;
            }
        }

        [ProtoMember(8), DefaultValue(null)]
        public ChannelMessage ChannelMsg
        {
            get
            {
                return channelMsg;
            }
            set
            {
                channelMsg = value;
            }
        }

        [ProtoMember(9), DefaultValue(0L)]
        public long ForbidEndTime
        {
            get
            {
                return forbidEndTime;
            }
            set
            {
                forbidEndTime = value;
            }
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
        }

    }
}

