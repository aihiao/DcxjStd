using LywGames.Messages.Proto.Auth;
using LywGames.Corgi.Protocol;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_CA_ActiveCode, IsRequest = true)]
    public class CAActiveCodeMessage : ProtocolMessage<ActiveCodeReq>
    {
    }
}
