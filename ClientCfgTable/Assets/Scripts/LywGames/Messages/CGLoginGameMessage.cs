using LywGames.Corgi.Protocol;
using LywGames.Messages.Proto.Game;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_CG_GameLogin, IsRequest = true)]
    public class CGLoginGameMessage : ProtocolMessage<LoginGameReq>
    {
    }
}
