using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Game
{
    [ProtoContract(Name = "TableVersion")]
    public class TableVersion : IExtensible
    {
        private string _tableName = "";
        private int _versionId = 0;
        private string _url = "";
        private bool _isCompress = false;
        private IExtension extensionObject;

        [ProtoMember(1), DefaultValue("")]
        public string tableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        [ProtoMember(1), DefaultValue(0)]
        public int versionId
        {
            get
            {
                return this._versionId;
            }
            set
            {
                this._versionId = value;
            }
        }

        [ProtoMember(1), DefaultValue("")]
        public string url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        [ProtoMember(1), DefaultValue(false)]
        public bool isCompress
        {
            get
            {
                return this._isCompress;
            }
            set
            {
                this._isCompress = value;
            }
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
