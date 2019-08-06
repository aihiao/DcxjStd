using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = 131075, IsRequest = false)]
    public class GCLoginGameMessage : ProtocolMessage<LoginGameRes>
    {
        public override int ResultCode
        {
            get
            {
                return protocol.ResultCode;
            }
        }
    }
}
