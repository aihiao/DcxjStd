using LywGames.Corgi.Protocol;
using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_GC_GameLogin, IsRequest = false)]
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
