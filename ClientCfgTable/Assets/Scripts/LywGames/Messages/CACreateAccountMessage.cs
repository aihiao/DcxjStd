using LywGames.Messages.Proto.Auth;
using LywGames.Corgi.Protocol;

namespace LywGames.Messages
{
    [Message(ProtocolId = Protocols.P_CA_CreateAccount, IsRequest = true)]
    public class CACreateAccountMessage : ProtocolMessage<CreateAccountReq>
    {
    }
}
