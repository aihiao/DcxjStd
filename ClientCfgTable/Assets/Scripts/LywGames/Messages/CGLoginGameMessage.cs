using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = 131074, IsRequest = true)]
    public class CGLoginGameMessage : ProtocolMessage<LoginGameReq>
    {
    }
}
