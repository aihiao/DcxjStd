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
                return this.inHeader;
            }
        }

        public AbstractNetworkOutHandler OutHeader
        {
            get
            {
                return this.outHeader;
            }
        }

        public void AddHandler(AbstractNetworkInHandler handler)
        {
            Monitor.Enter(this);
            try
            {
                if (this.inHeader == null)
                {
                    this.inHeader = handler;
                }
                if (this.inTailer == null)
                {
                    this.inTailer = handler;
                }
                else
                {
                    this.inTailer.NextInHandler = handler;
                    this.inTailer = handler;
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
                if (this.outTailer == null)
                {
                    this.outTailer = handler;
                }
                handler.NextOutHandler = this.outHeader;
                this.outHeader = handler;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

    }
}
