using System.ComponentModel;
using System.Collections.Generic;
using ProtoBuf;

namespace LywGames.Messages.Proto.Game
{
    [ProtoContract(Name = "LoginGameRes")]
    public class LoginGameRes : IExtensible
    {
        private int _result;
        private long _roleId = 0L;
        private bool _hasGMPrivilege = false;
        private bool _needQueryData = false;
        private readonly List<TableVersion> _datas = new List<TableVersion>();
        private int _size = 0;
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
        public long roleId
        {
            get
            {
                return this._roleId;
            }
            set
            {
                this._roleId = value;
            }
        }
        [ProtoMember(1), DefaultValue(false)]
        public bool hasGMPrivilege
        {
            get
            {
                return this._hasGMPrivilege;
            }
            set
            {
                this._hasGMPrivilege = value;
            }
        }
        [ProtoMember(1), DefaultValue(false)]
        public bool needQueryData
        {
            get
            {
                return this._needQueryData;
            }
            set
            {
                this._needQueryData = value;
            }
        }
        [ProtoMember(1)]
        public List<TableVersion> datas
        {
            get
            {
                return this._datas;
            }
        }
        [ProtoMember(1), DefaultValue(0)]
        public int size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}