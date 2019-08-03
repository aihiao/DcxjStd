using LywGames.Network;
using LywGames.Messages;

namespace LywGames.ClientHelper
{
    public class ClientHelperMessageInitializer : AbstractMessageInitializer
    {
        public override void Initilial()
        {
            base.AddMessage(typeof(ACLoginAuthMessage));
       
            base.AddMessage(typeof(GCLoginGameMessage));
           
        }
    }
}
