using System.Net;
using System.Threading;
using LywGames.Messages;
using LywGames.Common;

namespace LywGames.Network
{
    public abstract class IConnection
    {
        public enum ConnectionStatus
        {
            CONNECTIONSTATUS_INIT,
            CONNECTIONSTATUS_CONNECTING,
            CONNECTIONSTATUS_CONNECTED,
            CONNECTIONSTATUS_DISCONNECTED,
            CONNECTIONSTATUS_CLOSEDBYLOGIC
        }

        protected NetworkHandlerPipeline handlerPipeline = new NetworkHandlerPipeline();
        protected int channelId = 0;
        protected long sendAmount;
        protected long receiveAmount;
        protected GCLoginGameMessage loginGameRes = null;
        protected IPEndPoint remote;
        protected ConnectionStatus connectionStatus = ConnectionStatus.CONNECTIONSTATUS_INIT;
        protected object statusLock = new object();

        public long SendAmount
        {
            get
            {
                return sendAmount;
            }
        }

        public long ReceiveAmount
        {
            get
            {
                return receiveAmount;
            }
        }

        public GCLoginGameMessage LoginGameRes
        {
            get
            {
                return loginGameRes;
            }
            set
            {
                loginGameRes = value;
            }
        }

        public IPEndPoint Remote
        {
            get
            {
                return remote;
            }
        }

        public ConnectionStatus getConnectionStatus()
        {
            object obj;
            Monitor.Enter(obj = this.statusLock);
            ConnectionStatus result;
            try
            {
                result = this.connectionStatus;
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        public abstract void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress);
        public abstract bool ConnectSync(IPEndPoint localAddress, IPEndPoint remoteAddress, int timeout);
        public abstract void Disconnect();

        public bool isConnected()
        {
            object obj;
            Monitor.Enter(obj = this.statusLock);
            bool result;
            try
            {
                result = (this.connectionStatus == IConnection.ConnectionStatus.CONNECTIONSTATUS_CONNECTED);
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        public bool isConnecting()
        {
            object obj;
            Monitor.Enter(obj = this.statusLock);
            bool result;
            try
            {
                result = (this.connectionStatus == IConnection.ConnectionStatus.CONNECTIONSTATUS_CONNECTING);
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        public bool isConnectionNonStart()
        {
            object obj;
            Monitor.Enter(obj = this.statusLock);
            bool result;
            try
            {
                result = (connectionStatus == ConnectionStatus.CONNECTIONSTATUS_CLOSEDBYLOGIC);
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        private bool Send(byte[] buffer, int offset, int count, int protocolId, int channelId)
        {
            bool result;
            if (!isConnected())
            {
                LoggerManager.Instance.Info("Send protocolId {0}-{0:X} but connected false", new object[]
                {
                    protocolId
                });
                result = false;
            }
            else
            {
                this.channelId = channelId;
                NetworkBuffer networkBuffer = new NetworkBuffer(count + 4, true);
                networkBuffer.Write(protocolId);
                networkBuffer.Write(buffer, offset, count);
                if (handlerPipeline.OutHeader != null)
                {
                    handlerPipeline.OutHeader.Send(this, networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
                    result = true;
                }
                else
                {
                    LoggerManager.Instance.Info("IConnection.Send data protocolId {0}-{0:X} then call_Send", new object[]
                    {
                        protocolId
                    });
                    result = this._Send(networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
                }
            }
            return result;
        }

        public bool Send(Message obj, int channelId = 1)
        {
            bool result;
            if (!isConnected())
            {
                LoggerManager.Instance.Warn("Send Message {0} found connection {1} is not connected", new object[]
                {
                    obj.ProtocolId,
                    remote
                });
                result = false;
            }
            else
            {
                this.channelId = channelId;
                if (handlerPipeline.OutHeader != null)
                {
                    handlerPipeline.OutHeader.Send(this, obj);
                    if (obj.ProtocolId != 131074)
                    {
                        sendAmount += 1L;
                    }
                    LoggerManager.Instance.Info("IConnection->Send Message protocolId {0}-{0:X} sendAmount {1}", new object[]
                    {
                        obj.ProtocolId,
                        sendAmount
                    });
                    result = true;
                }
                else
                {
                    LoggerManager.Instance.Info("IConnection->Send Message protocolId {0}-{0:X} but outheader null", new object[]
                    {
                        obj.ProtocolId
                    });
                    result = false;
                }
            }
            return result;
        }

        internal abstract bool _Send(byte[] buffer, int offset, int count);

        public abstract void Update();

        public void SetNetworkInitializer(AbstractNetworkInitializer networkInitilializer, ConnectionType cnType)
        {
            networkInitilializer.Initial(this.handlerPipeline, cnType);
        }

        public void AddNetworkHandler(AbstractNetworkInHandler handler)
        {
            handlerPipeline.AddHandler(handler);
        }

        public void AddNetworkHandler(AbstractNetworkOutHandler handler)
        {
            handlerPipeline.AddHandler(handler);
        }

        public void recvProtocol(int protocolId)
        {
            switch (protocolId)
            {
                case 131075:
                case 131077:
                    break;
                case 131076:
                    goto IL_27;
                default:
                    if (protocolId != 131307)
                    {
                        goto IL_27;
                    }
                    break;
            }
            return;
            IL_27:
            receiveAmount += 1L;
            LoggerManager.Instance.Info("recv protocol {0} recvAmount {1}", new object[]
            {
                protocolId,
                receiveAmount
            });
        }

        public void clearProtocolAmount()
        {
            sendAmount = 0L;
            receiveAmount = 0L;
        }

        public abstract long getNetworkPing();
        public abstract long getAliasedPing();
        public abstract bool getSendStatics(out uint sendBytes, out uint sendNum, out long totalTime);
        public abstract bool getRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime);
        public abstract void pauseStatics();
        public abstract void resumeStatics();
    }
}
