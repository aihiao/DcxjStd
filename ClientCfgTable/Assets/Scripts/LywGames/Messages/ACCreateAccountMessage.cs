using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65541, IsRequest = false)]
    public class ACCreateAccountMessage : ProtocolMessage<CreateAccountRes>
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
