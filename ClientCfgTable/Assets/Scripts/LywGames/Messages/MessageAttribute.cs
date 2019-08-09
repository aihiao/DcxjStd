using System;

namespace LywGames.Messages
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = true)]
    public class MessageAttribute : Attribute
    {
        private int protocolId;
        public int ProtocolId
        {
            get
            {
                return protocolId;
            }
            set
            {
                protocolId = value;
            }
        }

        private bool isRequest;
        public bool IsRequest
        {
            get
            {
                return isRequest;
            }
            set
            {
                isRequest = value;
            }
        }

    }
}
