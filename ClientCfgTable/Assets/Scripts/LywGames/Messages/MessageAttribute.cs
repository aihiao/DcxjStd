using System;

namespace LywGames.Messages
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = true)]
    public class MessageAttribute : Attribute
    {
        private int protocolId;
        private bool isRequest;
        public int ProtocolId
        {
            get
            {
                return this.protocolId;
            }
            set
            {
                this.protocolId = value;
            }
        }

        public bool IsRequest
        {
            get
            {
                return this.isRequest;
            }
            set
            {
                this.isRequest = value;
            }
        }

    }
}
