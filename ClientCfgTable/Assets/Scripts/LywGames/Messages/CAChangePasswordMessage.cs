using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65544, IsRequest = true)]
    public class CAChangePasswordMessage : ProtocolMessage<ChangePasswordReq>
    {
    }
}
