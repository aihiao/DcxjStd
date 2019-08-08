using LywGames.Network;
using LywGames.Messages;

namespace LywGames.ClientHelper
{
    public class ClientHelperMessageInitializer : AbstractMessageInitializer
    {
        public override void Initilial()
        {
            AddMessageType(typeof(ACCreateAccountMessage));
            AddMessageType(typeof(ACLoginAuthMessage));
            AddMessageType(typeof(ACActiveCodeMessage));
       
            AddMessageType(typeof(GCLoginGameMessage));
           
        }
    }
}
