using System.ComponentModel;
using System.Collections.Generic;
using ProtoBuf;

namespace LywGames.Messages.Proto.Game
{
    [ProtoContract(Name = "LoginGameReq")]
    public class LoginGameReq : IExtensible
    {
        private string _token;
        private long _accountId;
        private int _areaId;
        private readonly List<TableVersion> _datas = new List<TableVersion>();
        private long _sendProtocolAmount = 0L;
        private long _recvProtocolAmount = 0L;
        private IExtension extensionObject;
        [ProtoMember(1)]
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
        [ProtoMember(1)]
        public int areaId
        {
            get
            {
                return this._areaId;
            }
            set
            {
                this._areaId = value;
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
        [ProtoMember(1), DefaultValue(0L)]
        public long sendProtocolAmount
        {
            get
            {
                return this._sendProtocolAmount;
            }
            set
            {
                this._sendProtocolAmount = value;
            }
        }
        [ProtoMember(1), DefaultValue(0L)]
        public long recvProtocolAmount
        {
            get
            {
                return this._recvProtocolAmount;
            }
            set
            {
                this._recvProtocolAmount = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
