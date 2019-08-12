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
                LoggerManager.Instance.Error(message);
            }
            else
            {
                if (level == 16u)
                {
                    LoggerManager.Instance.Warn(message);
                }
                else
                {
                    if (level == 1u)
                    {
                        LoggerManager.Instance.Info(message);
                    }
                    else
                    {
                        LoggerManager.Instance.Debug(message);
                    }
                }
            }
        }

        public UdpAdpaterConnection(int userdata)
        {
            UdpLog.SetWriter(new UdpLog.Writer(UdpLogWriter));

            UdpPlatform udpPlatform = new DotNetPlatform();
            socket = new UdpSocket(udpPlatform);
            CreateStreamChannel(1, "Default.Udp.StreamChannel", true, 9);
            socket.Start(UdpEndPoint.Any, UdpSocketMode.Client);
            totalTime = 0L;
            beginTime = 0L;
        }

        public override void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress)
        {
            udpRemote = new UdpEndPoint(new UdpIPv4Address(NetUtil.GetLongAddress(remoteAddress.Address.GetAddressBytes())), (ushort)remoteAddress.Port);
            remote = remoteAddress;
            if (!IsConnected() && !IsConnecting())
            {
                socket.Connect(udpRemote);
                object statusLock;
                Monitor.Enter(statusLock = this.statusLock);
                try
                {
                    connectionStatus = ConnectionStatus.Connecting;
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
            if (IsConnected())
            {
                UdpIPv4Address udpIPv4Address = new UdpIPv4Address(NetUtil.GetLongAddress(remoteAddress.Address.GetAddressBytes()));
                UdpIPv4Address address = this.udpRemote.Address;
                if (address.Equals(udpIPv4Address) && (int)udpRemote.Port == remoteAddress.Port)
                {
                    result = true;
                    return result;
                }
                LoggerManager.Instance.Warn("ConnectSync found already connected new addr {0} old addr {1}", remoteAddress.ToString(), udpRemote.ToString());
            }

            if (!IsConnecting())
            {
                udpRemote = new UdpEndPoint(new UdpIPv4Address(NetUtil.GetLongAddress(remoteAddress.Address.GetAddressBytes())), (ushort)remoteAddress.Port);
                remote = remoteAddress;
                socket.Connect(udpRemote);
            }

            object obj = statusLock;
            Monitor.Enter(obj);
            try
            {
                connectionStatus = ConnectionStatus.Connecting;
            }
            finally
            {
                Monitor.Exit(obj);
            }

            long num2 = DateTime.Now.ToFileTime() / 10000L;
            while (num2 - num < (long)(timeout * 1000))
            {
                UdpEvent ev;
                if (socket.Poll(out ev))
                {
                    UdpEventType eventType = ev.EventType;
                    switch (eventType)
                    {
                        case UdpEventType.ConnectFailed:
                        case UdpEventType.ConnectRefused:
                        case UdpEventType.Connected:
                            ProcessConnect(ev);
                            result = (ev.EventType == UdpEventType.Connected);
                            return result;
                        case (UdpEventType)5:
                        case (UdpEventType)7:
                            break;
                        default:
                            if (eventType == UdpEventType.ServerForceQuit)
                            {
                                ProcessServerForceQuit(ev);
                                result = false;
                                return result;
                            }
                            break;
                    }
                }
                Thread.Sleep(10);
                num2 = DateTime.Now.ToFileTime() / 10000L;
            }

            Monitor.Enter(obj = this.statusLock);
            try
            {
                connectionStatus = ConnectionStatus.Disconnected;
            }
            finally
            {
                Monitor.Exit(obj);
            }
            result = false;
            return result;
        }

        public override void Disconnect()
        {
            LoggerManager.Instance.Debug("Logic call Disconnect");

            object obj = statusLock;
            Monitor.Enter(obj);
            try
            {
                connectionStatus = ConnectionStatus.ClosedByLogic;
            }
            finally
            {
                Monitor.Exit(obj);
            }

            if (connection != null)
            {
                connection.Disconnect(null);
                connection = null;
            }

            if (socket != null)
            {
                socket.Close();
            }
        }

        internal override bool Send(byte[] buffer, int offset, int count)
        {
            byte[] array = new byte[count];
            Array.Copy(buffer, 0, array, 0, count);

            bool result;
            if (channelId != 0)
            {
                int num = 0;
                while (!connection.StreamBytes(channelId, array))
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
            while (socket.Poll(out ev))
            {
                UdpEventType eventType = ev.EventType;
                if (eventType <= UdpEventType.PacketReceived)
                {
                    switch (eventType)
                    {
                        case UdpEventType.ConnectFailed:
                        case UdpEventType.ConnectRefused:
                        case UdpEventType.Connected:
                            ProcessConnect(ev);
                            break;
                        case (UdpEventType)5:
                        case (UdpEventType)7:
                        case (UdpEventType)9:
                            break;
                        case UdpEventType.Disconnected:
                            ProcessDisconnect(ev);
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
                        ProcessServerForceQuit(ev);
                    }
                }
                continue;
                IL_5C:
                ProcessReceive(ev);
            }
        }
        private void ProcessConnect(UdpEvent ev)
        {
            if (ev.Connection != null)
            {
                LoggerManager.Instance.Debug("ProcessConnect connect to server remote {0} result {1}", ev.Connection.RemoteEndPoint, ev.EventType);
            }
            else
            {
                LoggerManager.Instance.Debug("ProcessConnect connect to server result {0}", ev.EventType);
            }

            if (ev.EventType == UdpEventType.Connected)
            {
                object obj = statusLock;
                Monitor.Enter(obj);
                try
                {
                    connectionStatus = ConnectionStatus.Connected;
                }
                finally
                {
                    Monitor.Exit(obj);
                }

                connection = ev.Connection;
                if (handlerPipeline.InHeader != null)
                {
                    handlerPipeline.InHeader.OnConnected(this, SocketError.Success);
                }
            }
            else
            {
                object obj = statusLock;
                Monitor.Enter(obj);
                try
                {
                    connectionStatus = ConnectionStatus.Disconnected;
                }
                finally
                {
                    Monitor.Exit(obj);
                }

                if (handlerPipeline.InHeader != null)
                {
                    handlerPipeline.InHeader.OnConnected(this, SocketError.SocketError);
                }
            }
        }

        private void ProcessReceive(UdpEvent ev)
        {
            if (ev.EventType == UdpEventType.StreamDataReceived)
            {
                byte[] data = ev.StreamOp.Data;
                int size = ev.StreamOp.Data.Length;
                if (handlerPipeline.InHeader != null)
                {
                    try
                    {
                        handlerPipeline.InHeader.OnReceived(this, data, 0, size);
                    }
                    catch (Exception ex)
                    {
                        LoggerManager.Instance.Error("ProcessReceive " + ex.ToString());
                    }
                }
            }
        }

        private void ProcessDisconnect(UdpEvent ev)
        {
            LoggerManager.Instance.Debug("ProcessDisconnect Disconnected from server at {0}", ev.Connection.RemoteEndPoint);

            lastConnection = connection;
            if (handlerPipeline.InHeader != null)
            {
                handlerPipeline.InHeader.OnDisconnected(this, SocketError.ConnectionAborted);
            }

            object obj = statusLock;
            Monitor.Enter(obj);
            try
            {
                connectionStatus = ConnectionStatus.Disconnected;
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        private void ProcessServerForceQuit(UdpEvent ev)
        {
            LoggerManager.Instance.Debug("ProcessServerForceQuit server {0} force quit me", ev.Connection.RemoteEndPoint);

            lastConnection = null;
            if (handlerPipeline.InHeader != null)
            {
                handlerPipeline.InHeader.OnDisconnected(this, SocketError.ConnectionAborted);
            }

            object obj = statusLock;
            Monitor.Enter(obj);
            try
            {
                connectionStatus = ConnectionStatus.Disconnected;
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        public override long GetNetworkPing()
        {
            long result;
            if (connection == null)
            {
                result = -1L;
            }
            else
            {
                result = (long)(connection.NetworkPing * 1000f);
            }
            return result;
        }

        public override long GetAliasedPing()
        {
            long result;
            if (connection == null)
            {
                result = -1L;
            }
            else
            {
                result = (long)(connection.AliasedPing * 1000f);
            }
            return result;
        }

        public override bool GetSendStatics(out uint sendBytes, out uint sendNum, out long totalTime)
        {
            bool result;
            if (connection == null)
            {
                sendBytes = 0u;
                sendNum = 0u;
                totalTime = 0L;
                result = false;
            }
            else
            {
                connection.getSendInfo(out sendBytes, out sendNum);
                totalTime = this.totalTime;
                if (beginTime != 0L)
                {
                    long num = DateTime.Now.ToFileTime() / 10000L;
                    totalTime += num - beginTime;
                }
                result = true;
            }
            return result;
        }

        public override bool GetRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime)
        {
            bool result;
            if (connection == null)
            {
                recvBytes = 0u;
                recvNum = 0u;
                totalTime = 0L;
                result = false;
            }
            else
            {
                connection.getRecvInfo(out recvBytes, out recvNum);
                totalTime = this.totalTime;
                if (beginTime != 0L)
                {
                    long num = DateTime.Now.ToFileTime() / 10000L;
                    totalTime += num - beginTime;
                }
                result = true;
            }
            return result;
        }

        public override void PauseStatics()
        {
            if (connection != null)
            {
                long num = DateTime.Now.ToFileTime() / 10000L - beginTime;
                if (num > 0L && beginTime != 0L)
                {
                    totalTime += num;
                }
                else
                {
                    beginTime = 0L;
                }
            }
        }

        public override void ResumeStatics()
        {
            beginTime = DateTime.Now.ToFileTime() / 10000L;
        }

    }
}
