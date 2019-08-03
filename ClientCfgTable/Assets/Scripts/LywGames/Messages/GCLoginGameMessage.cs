using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = 131075, IsRequest = false)]
    public class GCLoginGameMessage : ProtocolMessage<LoginGameRes>
    {
        public override int Result
        {
            get
            {
                return protocol.result;
            }
        }
    }
}
