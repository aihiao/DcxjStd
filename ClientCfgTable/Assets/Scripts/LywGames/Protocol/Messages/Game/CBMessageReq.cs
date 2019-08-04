using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Game
{
    [ProtoContract(Name = "CBMessageReq")]
    public class CBMessageReq : IExtensible
    {
        private byte[] _Buffer = null;
        private IExtension extensionObject;
        [ProtoMember(1), DefaultValue(null)]
        public byte[] Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }
        public byte[] getBuffer()
        {
            return this._Buffer;
        }
        public void setBuffer(byte[] value)
        {
            this._Buffer = value;
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
