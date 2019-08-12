using LywGames.Messages.Proto.Auth;
using LywGames.Corgi.Protocol;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_CA_BindAccount, IsRequest = true)]
    public class CABindAccountMessage : ProtocolMessage<BindAccountReq>
    {
    }
}
