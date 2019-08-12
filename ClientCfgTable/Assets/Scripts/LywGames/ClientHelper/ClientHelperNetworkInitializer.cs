using System.Net.Sockets;
using LywGames.Network;

namespace LywGames.ClientHelper
{
    public class ClientHelperNetworkInitializer : AbstractNetworkInitializer
    {
        private AbstractMessageInitializer msgInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        private ProtocolType protocolType;

        public ClientHelperNetworkInitializer(AbstractMessageInitializer msgInitializer, MessageDelegateProcessor msgDelegateProcessor, ProtocolType protocolType)
        {
            this.msgInitializer = msgInitializer;
            this.msgDelegateProcessor = msgDelegateProcessor;
            this.protocolType = protocolType;
        }

        public override void Initial(NetworkHandlerPipeline pipeline, ConnectionType cnType)
        {
            if (protocolType != ProtocolType.Udp || (cnType != ConnectionType.Game && cnType != ConnectionType.Battle))
            {
                pipeline.AddHandler(new LengthFieldBasedFrameDecoder(204800, 0, 4, 0, 4));
                pipeline.AddHandler(new LengthFieldPrepender(4, 0, false));
            }
            pipeline.AddHandler(new SnappyDecoder());
            pipeline.AddHandler(new SnappyEncoder());
            pipeline.AddHandler(new MessageInProcessor(msgInitializer, msgDelegateProcessor));
            pipeline.AddHandler(new MessageOutProcessor());
        }

    }
}
