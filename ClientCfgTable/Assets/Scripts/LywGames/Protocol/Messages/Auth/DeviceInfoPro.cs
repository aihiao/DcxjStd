using System.ComponentModel;
using ProtoBuf;

namespace LywGames.Messages.Proto.Auth
{
    [ProtoContract(Name = "DeviceInfoPro")]
    public class DeviceInfoPro : IExtensible
    {
        private string _OSType = "";
        private string _OSVersion = "";
        private int _deviceType = 0;
        private string _UDID = "";
        private string _deviceName = "";
        private IExtension extensionObject;
        [ProtoMember(1), DefaultValue("")]
        public string OSType
        {
            get
            {
                return this._OSType;
            }
            set
            {
                this._OSType = value;
            }
        }
        [ProtoMember(1), DefaultValue("")]
        public string OSVersion
        {
            get
            {
                return this._OSVersion;
            }
            set
            {
                this._OSVersion = value;
            }
        }
        [ProtoMember(1), DefaultValue(0)]
        public int deviceType
        {
            get
            {
                return this._deviceType;
            }
            set
            {
                this._deviceType = value;
            }
        }
        [ProtoMember(1), DefaultValue("")]
        public string UDID
        {
            get
            {
                return this._UDID;
            }
            set
            {
                this._UDID = value;
            }
        }
        [ProtoMember(1), DefaultValue("")]
        public string deviceName
        {
            get
            {
                return this._deviceName;
            }
            set
            {
                this._deviceName = value;
            }
        }
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}
