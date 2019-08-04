using System.Net.Sockets;
using LywGames.Network;

namespace LywGames.ClientHelper
{
    public class ClientHelperNetworkInitializer : AbstractNetworkInitializer
    {
        private AbstractMessageInitializer msgInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        private ProtocolType type;
        public ClientHelperNetworkInitializer(AbstractMessageInitializer msgInitializer, MessageDelegateProcessor msgDelegateProcessor, ProtocolType type)
        {
            this.msgInitializer = msgInitializer;
            this.msgDelegateProcessor = msgDelegateProcessor;
            this.type = type;
        }
        public override void Initial(NetworkHandlerPipeline pipeline, ConnectionType cnType)
        {
            if (this.type != ProtocolType.Udp || (cnType != ConnectionType.CONNECTION_GAME && cnType != ConnectionType.CONNECTION_BATTLE))
            {
                pipeline.AddHandler(new LengthFieldBasedFrameDecoder(204800, 0, 4, 0, 4));
                pipeline.AddHandler(new LengthFieldPrepender(4, 0, false));
            }
            pipeline.AddHandler(new SnappyDecoder());
            pipeline.AddHandler(new SnappyEncoder());
            pipeline.AddHandler(new MessageInProcessor(this.msgInitializer, this.msgDelegateProcessor));
            pipeline.AddHandler(new MessageOutProcessor());
        }
    }
}
