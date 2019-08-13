using LywGames.Messages.Proto.Auth;
using LywGames.Corgi.Protocol;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_AC_AuthLogin, IsRequest = false)]
    public class ACLoginAuthMessage : ProtocolMessage<LoginRes>
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
