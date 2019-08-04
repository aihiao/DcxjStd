using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UdpKit;

namespace LywGames.Network
{
    public class UdpAdpaterConnection : IConnection
    {
        private UdpSocket socket;
        private UdpConnection connection = null;
        private UdpConnection lastConnection = null;
        private UdpEndPoint udpRemote;
        private long beginTime;
        private long totalTime;

        public void UdpLogWriter(uint level, string message)
        {
            if (level == 0u)
            {
                LoggerManager.Instance.Error(message, new object[0]);
            }
            else
            {
                if (level == 16u)
                {
                    LoggerManager.Instance.Warn(message, new object[0]);
                }
                else
                {
                    if (level == 1u)
                    {
                        LoggerManager.Instance.Info(message, new object[0]);
                    }
                    else
                    {
                        LoggerManager.Instance.Debug(message, new object[0]);
                    }
                }
            }
        }

        public UdpAdpaterConnection(int userdata)
        {
            UdpLog.SetWriter(new UdpLog.Writer(this.UdpLogWriter));
            UdpPlatform udpPlatform = new DotNetPlatform();
            this.socket = new UdpSocket(udpPlatform);
            this.CreateStreamChannel(1, "Default.Udp.StreamChannel", true, 9);
            this.socket.Start(UdpEndPoint.Any, UdpSocketMode.Client);
            this.totalTime = 0L;
            this.beginTime = 0L;
        }

        public override void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress)
        {
            this.udpRemote = new UdpEndPoint(new UdpIPv4Address(remoteAddress.Address.Address), (ushort)remoteAddress.Port);
            this.remote = remoteAddress;
            if (!base.isConnected() && !base.isConnecting())
            {
                this.socket.Connect(this.udpRemote);
                object statusLock;
                Monitor.Enter(statusLock = this.statusLock);
                try
                {
                    this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_CONNECTING;
                }
                finally
                {
                    Monitor.Exit(statusLock);
                }
            }
        }
        public override bool ConnectSync(IPEndPoint localAddress, IPEndPoint remoteAddress, int timeout)
        {
            long num = DateTime.Now.ToFileTime() / 10000L;
            bool result;
            if (base.isConnected())
            {
                UdpIPv4Address udpIPv4Address = new UdpIPv4Address(remoteAddress.Address.Address);
                UdpIPv4Address address = this.udpRemote.Address;
                if (address.Equals(udpIPv4Address) && (int)this.udpRemote.Port == remoteAddress.Port)
                {
                    result = true;
                    return result;
                }
                LoggerManager.Instance.Warn("ConnectSync found already connected new addr {0} old addr {1}", new object[]
                {
                    remoteAddress.ToString(),
                    this.udpRemote.ToString()
                });
            }
            if (!base.isConnecting())
            {
                this.udpRemote = new UdpEndPoint(new UdpIPv4Address(remoteAddress.Address.Address), (ushort)remoteAddress.Port);
                this.remote = remoteAddress;
                this.socket.Connect(this.udpRemote);
            }
            object statusLock;
            Monitor.Enter(statusLock = this.statusLock);
            try
            {
                this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_CONNECTING;
            }
            finally
            {
                Monitor.Exit(statusLock);
            }
            long num2 = DateTime.Now.ToFileTime() / 10000L;
            while (num2 - num < (long)(timeout * 1000))
            {
                UdpEvent ev;
                if (this.socket.Poll(out ev))
                {
                    UdpEventType eventType = ev.EventType;
                    switch (eventType)
                    {
                        case UdpEventType.ConnectFailed:
                        case UdpEventType.ConnectRefused:
                        case UdpEventType.Connected:
                            this.ProcessConnect(ev);
                            result = (ev.EventType == UdpEventType.Connected);
                            return result;
                        case (UdpEventType)5:
                        case (UdpEventType)7:
                            break;
                        default:
                            if (eventType == UdpEventType.ServerForceQuit)
                            {
                                this.ProcessServerForceQuit(ev);
                                result = false;
                                return result;
                            }
                            break;
                    }
                }
                Thread.Sleep(10);
                num2 = DateTime.Now.ToFileTime() / 10000L;
            }
            Monitor.Enter(statusLock = this.statusLock);
            try
            {
                this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED;
            }
            finally
            {
                Monitor.Exit(statusLock);
            }
            result = false;
            return result;
        }

        public override void Disconnect()
        {
            LoggerManager.Instance.Debug("Logic call Disconnect", new object[0]);
            object statusLock;
            Monitor.Enter(statusLock = this.statusLock);
            try
            {
                this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_CLOSEDBYLOGIC;
            }
            finally
            {
                Monitor.Exit(statusLock);
            }
            if (this.connection != null)
            {
                this.connection.Disconnect(null);
                this.connection = null;
            }
            if (this.socket != null)
            {
                this.socket.Close();
            }
        }

        internal override bool _Send(byte[] buffer, int offset, int count)
        {
            byte[] array = new byte[count];
            Array.Copy(buffer, 0, array, 0, count);
            bool result;
            if (this.channelId != 0)
            {
                int num = 0;
                while (!this.connection.StreamBytes(this.channelId, array))
                {
                    num++;
                    if (num > 5)
                    {
                        result = false;
                        return result;
                    }
                    Thread.Sleep(1);
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private bool CreateStreamChannel(int channelId, string channelName, bool isReliable, int priority)
        {
            if (!isReliable)
            {
            }
            return true;
        }

        public override void Update()
        {
            UdpEvent ev;
            while (this.socket.Poll(out ev))
            {
                UdpEventType eventType = ev.EventType;
                if (eventType <= UdpEventType.PacketReceived)
                {
                    switch (eventType)
                    {
                        case UdpEventType.ConnectFailed:
                        case UdpEventType.ConnectRefused:
                        case UdpEventType.Connected:
                            this.ProcessConnect(ev);
                            break;
                        case (UdpEventType)5:
                        case (UdpEventType)7:
                        case (UdpEventType)9:
                            break;
                        case UdpEventType.Disconnected:
                            this.ProcessDisconnect(ev);
                            break;
                        default:
                            if (eventType == UdpEventType.PacketReceived)
                            {
                                goto IL_5C;
                            }
                            break;
                    }
                }
                else
                {
                    if (eventType != UdpEventType.ServerForceQuit)
                    {
                        if (eventType == UdpEventType.StreamDataReceived)
                        {
                            goto IL_5C;
                        }
                    }
                    else
                    {
                        this.ProcessServerForceQuit(ev);
                    }
                }
                continue;
                IL_5C:
                this.ProcessReceive(ev);
            }
        }
        private void ProcessConnect(UdpEvent ev)
        {
            bool arg_1F_0;
            if (ev.Connection != null)
            {
                UdpEndPoint arg_16_0 = ev.Connection.RemoteEndPoint;
                arg_1F_0 = (1 == 0);
            }
            else
            {
                arg_1F_0 = true;
            }

            if (!arg_1F_0)
            {
                LoggerManager.Instance.Debug("ProcessConnect connect to server remote {0} result {1}", new object[]
                {
                    ev.Connection.RemoteEndPoint,
                    ev.EventType
                });
            }
            else
            {
                LoggerManager.Instance.Debug("ProcessConnect connect to server result {0}", new object[]
                {
                    ev.EventType
                });
            }

            if (ev.EventType == UdpEventType.Connected)
            {
                object statusLock;
                Monitor.Enter(statusLock = this.statusLock);
                try
                {
                    this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_CONNECTED;
                }
                finally
                {
                    Monitor.Exit(statusLock);
                }
                this.connection = ev.Connection;
                if (this.handlerPipeline.InHeader != null)
                {
                    this.handlerPipeline.InHeader.OnConnected(this, SocketError.Success);
                }
            }
            else
            {
                object statusLock;
                Monitor.Enter(statusLock = this.statusLock);
                try
                {
                    this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED;
                }
                finally
                {
                    Monitor.Exit(statusLock);
                }
                if (this.handlerPipeline.InHeader != null)
                {
                    this.handlerPipeline.InHeader.OnConnected(this, SocketError.SocketError);
                }
            }
        }

        private void ProcessReceive(UdpEvent ev)
        {
            if (ev.EventType == UdpEventType.StreamDataReceived)
            {
                byte[] data = ev.StreamOp.Data;
                int size = ev.StreamOp.Data.Length;
                if (this.handlerPipeline.InHeader != null)
                {
                    try
                    {
                        this.handlerPipeline.InHeader.OnReceived(this, data, 0, size);
                    }
                    catch (Exception ex)
                    {
                        LoggerManager.Instance.Error("ProcessReceive " + ex.ToString(), new object[0]);
                    }
                }
            }
        }

        private void ProcessDisconnect(UdpEvent ev)
        {
            this.lastConnection = this.connection;
            LoggerManager.Instance.Debug("ProcessDisconnect Disconnected from server at {0}", new object[]
            {
                ev.Connection.RemoteEndPoint
            });
            if (this.handlerPipeline.InHeader != null)
            {
                this.handlerPipeline.InHeader.OnDisconnected(this, SocketError.ConnectionAborted);
            }
            object statusLock;
            Monitor.Enter(statusLock = this.statusLock);
            try
            {
                this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED;
            }
            finally
            {
                Monitor.Exit(statusLock);
            }
        }

        private void ProcessServerForceQuit(UdpEvent ev)
        {
            this.lastConnection = null;
            LoggerManager.Instance.Debug("ProcessServerForceQuit server {0} force quit me", new object[]
            {
                ev.Connection.RemoteEndPoint
            });
            if (this.handlerPipeline.InHeader != null)
            {
                this.handlerPipeline.InHeader.OnDisconnected(this, SocketError.ConnectionAborted);
            }
            object statusLock;
            Monitor.Enter(statusLock = this.statusLock);
            try
            {
                this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED;
            }
            finally
            {
                Monitor.Exit(statusLock);
            }
        }

        public override long getNetworkPing()
        {
            long result;
            if (this.connection == null)
            {
                result = -1L;
            }
            else
            {
                result = (long)(this.connection.NetworkPing * 1000f);
            }
            return result;
        }

        public override long getAliasedPing()
        {
            long result;
            if (this.connection == null)
            {
                result = -1L;
            }
            else
            {
                result = (long)(this.connection.AliasedPing * 1000f);
            }
            return result;
        }

        public override bool getSendStatics(out uint sendBytes, out uint sendNum, out long totalTime)
        {
            bool result;
            if (this.connection == null)
            {
                sendBytes = 0u;
                sendNum = 0u;
                totalTime = 0L;
                result = false;
            }
            else
            {
                this.connection.getSendInfo(out sendBytes, out sendNum);
                totalTime = this.totalTime;
                if (this.beginTime != 0L)
                {
                    long num = DateTime.Now.ToFileTime() / 10000L;
                    totalTime += num - this.beginTime;
                }
                result = true;
            }
            return result;
        }

        public override bool getRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime)
        {
            bool result;
            if (this.connection == null)
            {
                recvBytes = 0u;
                recvNum = 0u;
                totalTime = 0L;
                result = false;
            }
            else
            {
                this.connection.getRecvInfo(out recvBytes, out recvNum);
                totalTime = this.totalTime;
                if (this.beginTime != 0L)
                {
                    long num = DateTime.Now.ToFileTime() / 10000L;
                    totalTime += num - this.beginTime;
                }
                result = true;
            }
            return result;
        }

        public override void pauseStatics()
        {
            if (this.connection != null)
            {
                long num = DateTime.Now.ToFileTime() / 10000L - this.beginTime;
                if (num > 0L && this.beginTime != 0L)
                {
                    this.totalTime += num;
                }
                else
                {
                    this.beginTime = 0L;
                }
            }
        }

        public override void resumeStatics()
        {
            this.beginTime = DateTime.Now.ToFileTime() / 10000L;
        }

    }
}
