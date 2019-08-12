using LywGames.Messages.Proto.Auth;
using LywGames.Corgi.Protocol;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_CA_ChangePassword, IsRequest = true)]
    public class CAChangePasswordMessage : ProtocolMessage<ChangePasswordReq>
    {
    }
}
