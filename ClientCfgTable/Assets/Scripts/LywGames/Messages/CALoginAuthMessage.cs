using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65538, IsRequest = true)]
    public class CALoginAuthMessage : ProtocolMessage<LoginReq>
    {
    }
}
