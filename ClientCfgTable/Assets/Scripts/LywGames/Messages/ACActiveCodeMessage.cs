using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65547, IsRequest = false)]
    public class ACActiveCodeMessage : ProtocolMessage<ActiveCodeRes>
    {
        public override int ResultCode
        {
            get
            {
                return protocol.ResultCode;
            }
        }
    }
}
