using LywGames.Messages.Proto.Auth;
using LywGames.Corgi.Protocol;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_CA_AuthLogin, IsRequest = true)]
    public class CALoginAuthMessage : ProtocolMessage<LoginReq>
    {
    }
}
