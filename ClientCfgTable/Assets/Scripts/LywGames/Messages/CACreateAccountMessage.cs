using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65540, IsRequest = true)]
    public class CACreateAccountMessage : ProtocolMessage<CreateAccountReq>
    {
    }
}
