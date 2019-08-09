using System.Net;
using System.Threading;
using LywGames.Messages;
using LywGames.Common;
using LywGames.Corgi.Protocol;

namespace LywGames.Network
{
    public abstract class IConnection
    {
        public enum ConnectionStatus
        {
            Init,
            Connecting,
            Connected,
            Disconnected,
            ClosedByLogic
        }

        protected NetworkHandlerPipeline handlerPipeline = new NetworkHandlerPipeline();
        protected ConnectionStatus connectionStatus = ConnectionStatus.Init;
        protected object statusLock = new object();
        protected int channelId = 0;

        protected long sendAmount;
        public long SendAmount { get{ return sendAmount; } }

        protected long receiveAmount;
        public long ReceiveAmount { get{ return receiveAmount; } }

        protected GCLoginGameMessage loginGameRes = null;
        public GCLoginGameMessage LoginGameRes
        {
            get { return loginGameRes; }
            set { loginGameRes = value; }
        }

        protected IPEndPoint remote;
        public IPEndPoint Remote { get { return remote; } }

        public ConnectionStatus GetConnectionStatus()
        {
            object obj;
            Monitor.Enter(obj = statusLock);
            ConnectionStatus result;
            try
            {
                result = connectionStatus;
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        public bool IsConnected()
        {
            object obj;
            Monitor.Enter(obj = statusLock);
            bool result;
            try
            {
                result = (connectionStatus == ConnectionStatus.Connected);
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        public bool IsConnecting()
        {
            object obj;
            Monitor.Enter(obj = statusLock);
            bool result;
            try
            {
                result = (connectionStatus == ConnectionStatus.Connecting);
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return result;
        }

        public bool IsConnectionNonStart()
        {
            object obj;
            Monitor.Enter(obj = statusLock);
            bool result;
            try
            {
                result = (connectionStatus == ConnectionStatus.ClosedByLogic);
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
        public abstract void Update();

        internal abstract bool Send(byte[] buffer, int offset, int count);

        private bool Send(byte[] buffer, int offset, int count, int protocolId, int channelId)
        {
            bool result;
            if (!IsConnected())
            {
                LoggerManager.Instance.Info("Send protocolId {0}-{0:X} but connected false", protocolId);
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
                    LoggerManager.Instance.Info("IConnection.Send data protocolId {0}-{0:X} then call_Send", protocolId);
                    result = Send(networkBuffer.GetBuffer(), networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
                }
            }
            return result;
        }

        public bool Send(Message obj, int channelId = 1)
        {
            bool result;
            if (!IsConnected())
            {
                LoggerManager.Instance.Warn("Send Message {0} found connection {1} is not connected", obj.ProtocolId, remote);
                result = false;
            }
            else
            {
                this.channelId = channelId;
                if (handlerPipeline.OutHeader != null)
                {
                    handlerPipeline.OutHeader.Send(this, obj);
                    if (obj.ProtocolId != Protocols.P_CG_GameLogin)
                    {
                        sendAmount += 1L;
                    }
                    LoggerManager.Instance.Info("IConnection->Send Message protocolId {0}-{0:X} sendAmount {1}", obj.ProtocolId, sendAmount);
                    result = true;
                }
                else
                {
                    LoggerManager.Instance.Info("IConnection->Send Message protocolId {0}-{0:X} but outheader null", obj.ProtocolId);
                    result = false;
                }
            }
            return result;
        }

        public void SetNetworkInitializer(AbstractNetworkInitializer networkInitilializer, ConnectionType cnType)
        {
            networkInitilializer.Initial(handlerPipeline, cnType);
        }

        public void AddNetworkHandler(AbstractNetworkInHandler handler)
        {
            handlerPipeline.AddHandler(handler);
        }

        public void AddNetworkHandler(AbstractNetworkOutHandler handler)
        {
            handlerPipeline.AddHandler(handler);
        }

        public void RecvProtocol(int protocolId)
        {
            switch (protocolId)
            {
                case Protocols.P_GC_GameLogin:
                case Protocols.P_GC_GameLogout:
                    break;
                case Protocols.P_CG_GameLogout:
                    goto IL_27;
                default:
                    if (protocolId != Protocols.P_GC_StaminaBuyChange)
                    {
                        goto IL_27;
                    }
                    break;
            }
            return;
            IL_27:
            receiveAmount += 1L;
            LoggerManager.Instance.Info("recv protocol {0} recvAmount {1}", protocolId, receiveAmount);
        }

        public void ClearProtocolAmount()
        {
            sendAmount = 0L;
            receiveAmount = 0L;
        }

        public abstract long GetNetworkPing();
        public abstract long GetAliasedPing();
        public abstract bool GetSendStatics(out uint sendBytes, out uint sendNum, out long totalTime);
        public abstract bool GetRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime);
        public abstract void PauseStatics();
        public abstract void ResumeStatics();
    }
}
