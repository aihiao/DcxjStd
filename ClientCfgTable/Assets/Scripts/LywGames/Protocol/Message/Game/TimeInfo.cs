using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Message.Proto.Game
{
    [ProtoContract(Name = "TimeInfo")]
    public class TimeInfo : IExtensible
    {
        private long _millsecond = 0L;
        private long _timezone = 0L;
        private IExtension extensionObject;

        [ProtoMember(0), DefaultValue(0L)]
        public long millsecond
        {
            get
            {
                return this._millsecond;
            }
            set
            {
                this._millsecond = value;
            }
        }

        [ProtoMember(0), DefaultValue(0L)]
        public long timezone
        {
            get
            {
                return this._timezone;
            }
            set
            {
                this._timezone = value;
            }
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

    }
}