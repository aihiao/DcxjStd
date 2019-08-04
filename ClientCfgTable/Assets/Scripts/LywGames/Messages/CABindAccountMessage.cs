using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65542, IsRequest = true)]
    public class CABindAccountMessage : ProtocolMessage<BindAccountReq>
    {
    }
}
