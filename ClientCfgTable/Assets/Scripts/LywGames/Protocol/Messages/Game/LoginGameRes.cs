using System.ComponentModel;
using System.Collections.Generic;
using ProtoBuf;

namespace LywGames.Messages.Proto.Game
{
    [ProtoContract(Name = "LoginGameRes")]
    public class LoginGameRes : IExtensible
    {
        private int resultCode;
        private long roleId = 0L;
        private bool hasGMPrivilege = false;
        private bool needQueryData = false;
        private readonly List<TableVersion> datas = new List<TableVersion>();
        private int size = 0;
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

        [ProtoMember(1), DefaultValue(0L)]
        public long RoleId
        {
            get
            {
                return roleId;
            }
            set
            {
                roleId = value;
            }
        }

        [ProtoMember(1), DefaultValue(false)]
        public bool HasGMPrivilege
        {
            get
            {
                return hasGMPrivilege;
            }
            set
            {
                hasGMPrivilege = value;
            }
        }

        [ProtoMember(1), DefaultValue(false)]
        public bool NeedQueryData
        {
            get
            {
                return needQueryData;
            }
            set
            {
                needQueryData = value;
            }
        }

        [ProtoMember(1)]
        public List<TableVersion> Datas
        {
            get
            {
                return datas;
            }
        }

        [ProtoMember(1), DefaultValue(0)]
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
        }

    }
}