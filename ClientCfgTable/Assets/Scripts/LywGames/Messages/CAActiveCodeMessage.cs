using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65546, IsRequest = true)]
    public class CAActiveCodeMessage : ProtocolMessage<ActiveCodeReq>
    {
    }
}
