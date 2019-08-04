using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = 196609, IsRequest = false)]
    public class CBMessageReqMessage : ProtocolMessage<CBMessageReq>
    {
    }
}
