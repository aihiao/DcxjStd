using System.Threading;

namespace LywGames.Network
{
    public class NetworkHandlerPipeline
    {
        private AbstractNetworkInHandler inHeader;
        private AbstractNetworkInHandler inTailer;
        private AbstractNetworkOutHandler outHeader;
        private AbstractNetworkOutHandler outTailer;

        public AbstractNetworkInHandler InHeader
        {
            get
            {
                return inHeader;
            }
        }

        public AbstractNetworkOutHandler OutHeader
        {
            get
            {
                return outHeader;
            }
        }

        public void AddHandler(AbstractNetworkInHandler handler)
        {
            Monitor.Enter(this);
            try
            {
                if (inHeader == null)
                {
                    inHeader = handler;
                }

                if (inTailer == null)
                {
                    inTailer = handler;
                }
                else
                {
                    inTailer.NextInHandler = handler;
                    inTailer = handler;
                }
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public void AddHandler(AbstractNetworkOutHandler handler)
        {
            Monitor.Enter(this);
            try
            {
                if (outTailer == null)
                {
                    outTailer = handler;
                }
                handler.NextOutHandler = outHeader;
                outHeader = handler;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

    }
}
