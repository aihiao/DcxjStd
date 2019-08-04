using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = 131078, IsRequest = true)]
    public class CGQueryLoginGameMessage : ProtocolMessage<QueryLoginGameDataREQ>
    {
    }
}
