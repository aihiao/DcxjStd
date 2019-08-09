using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using LywGames.Messages;

namespace LywGames.Network
{
    public class TCPConnection : IConnection
    {
        public static int MaxEventNumber = 128;

        private Socket socket;
        private BufferManager bufferManager;
        private int evenCount;

        private object eventLock = new object();
        private Queue<SocketAsyncEventArgs> freeEventArgs = new Queue<SocketAsyncEventArgs>();
        private Queue<SocketAsyncEventArgs> processEventArgs = new Queue<SocketAsyncEventArgs>();
        private Queue<SocketAsyncEventArgs> appendEventArgs = new Queue<SocketAsyncEventArgs>();

        public TCPConnection(int userData)
        {
            // AddressFamily.InterNetwork IP版本4的地址。
            // SocketType.Stream 支持可靠、双向、基于连接的字节流,而不重复数据,也不保留边界.此类型的Socket与单个对方主机通信,并且在通信开始之前需要建立远程主机连接.Stream使用传输控制协议(ProtocolType.Tcp)和AddressFamily.InterNetwork地址族。
            // ProtocolType.Tcp 传输控制协议。
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true; // 不使用Nagle算法

            evenCount = 0;

            bufferManager = new BufferManager(MaxEventNumber * NetworkParameters._MAX_RECV_BUFFER_SIZE, NetworkParameters._MAX_RECV_BUFFER_SIZE);
            bufferManager.InitBuffer();
        }

        private SocketAsyncEventArgs GetEventArg(bool needBuffer)
        {
            object obj = eventLock;
            Monitor.Enter(obj);
            SocketAsyncEventArgs result;
            try
            {
                if (freeEventArgs.Count > 0)
                {
                    SocketAsyncEventArgs socketAsyncEventArgs = freeEventArgs.Dequeue();
                    if (needBuffer && socketAsyncEventArgs.Buffer == null)
                    {
                        if (!bufferManager.SetBuffer(socketAsyncEventArgs))
                        {
                            result = null;
                            return result;
                        }
                    }
                    else
                    {
                        if (!needBuffer && socketAsyncEventArgs.Buffer != null)
                        {
                            bufferManager.FreeBuffer(socketAsyncEventArgs);
                        }
                    }
                    result = socketAsyncEventArgs;
                }
                else
                {
                    if (evenCount < MaxEventNumber)
                    {
                        evenCount++;
                        SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
                        socketAsyncEventArgs.RemoteEndPoint = remote;
                        socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArgCompleted);
                        if (needBuffer)
                        {
                            if (!bufferManager.SetBuffer(socketAsyncEventArgs))
                            {
                                result = null;
                                return result;
                            }
                        }
                        result = socketAsyncEventArgs;
                    }
                    else
                    {
                        LoggerManager.Instance.Warn("GetEventArd found without free event, MaxEventCount {0}", MaxEventNumber);
                        result = null;
                    }
                }
            }
            finally
            {
                Monitor.Exit(obj);
            }

            return result;
        }

        private void PushFreeEventArg(SocketAsyncEventArgs eventArg, bool clearBuffer)
        {
            object obj = eventLock;
            Monitor.Enter(obj); // Monitor提供同步访问对象的机制。Monitor.Enter获取一个对象的锁。
            try
            {
                if (clearBuffer)
                {
                    if (eventArg.Buffer != null)
                    {
                        bufferManager.FreeBuffer(eventArg);
                    }
                    else
                    {
                        eventArg.SetBuffer(null, 0, 0);
                    }
                }
                freeEventArgs.Enqueue(eventArg);
            }
            finally
            {
                Monitor.Exit(obj); // Monitor.Exit释放对象锁。
            }
        }

        private void PushAppendEventArg(SocketAsyncEventArgs eventArg)
        {
            object obj = eventLock;
            Monitor.Enter(obj);
            try
            {
                appendEventArgs.Enqueue(eventArg);
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        public override void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress)
        {
            LoggerManager.Instance.Debug("TCPConnection.connectAsync remote {0}", remoteAddress);
            remote = remoteAddress;

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

            SocketAsyncEventArgs eventArg = GetEventArg(false);
            if (eventArg == null)
            {
                LoggerManager.Instance.Error("Can't eventArg when connecting");

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
            else
            {
                if (!socket.ConnectAsync(eventArg))
                {
                    ProcessConnect(eventArg, false);
                }
            }
        }

        public override bool ConnectSync(IPEndPoint localAddress, IPEndPoint remoteAddress, int timeout)
        {
            long num = DateTime.Now.ToFileTime() / 10000L;
            remote = remoteAddress;

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

            SocketAsyncEventArgs eventArg = GetEventArg(false);
            if (eventArg == null)
            {
                LoggerManager.Instance.Error("Can't eventArg when connecting");

                Monitor.Enter(obj = statusLock);
                try
                {
                    connectionStatus = ConnectionStatus.Disconnected;
                }
                finally
                {
                    Monitor.Exit(obj);
                }

                return false;
            }
            else
            {
                if (!socket.ConnectAsync(eventArg))
                {
                    ProcessConnect(eventArg, false);
                }

                long num2 = DateTime.Now.ToFileTime() / 10000L;
                while (num2 - num < (long)(timeout * 1000))
                {
                    if (connectionStatus == ConnectionStatus.Connected)
                    {
                        return true;
                    }
                    Thread.Sleep(100);
                    num2 = DateTime.Now.ToFileTime() / 10000L;
                }

                return false;
            }
        }

        public override void Disconnect()
        {
            if (socket != null)
            {
                try
                {
                    LoggerManager.Instance.Debug("Logic call Disconnect {0}", socket.RemoteEndPoint);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(true);

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
                }
                catch (Exception e)
                {
                    LoggerManager.Instance.Error(e.ToString());
                }
            }
        }

        internal override bool Send(byte[] buffer, int offset, int count)
        {
            bool result;
            if (socket == null || !socket.Connected)
            {
                LoggerManager.Instance.Warn("TCPConnection._Send data len {0} but socket is not connected", count);
                result = false;
            }
            else
            {
                SocketAsyncEventArgs eventArg = GetEventArg(false);
                if (eventArg == null)
                {
                    LoggerManager.Instance.Error("There is no SocketAsyncEventArgs available");
                    result = false;
                }
                else
                {
                    if (!bufferManager.SetBuffer(eventArg, buffer, offset, count))
                    {
                        LoggerManager.Instance.Error("There is no memory available in buffermanager");
                        result = false;
                    }
                    else
                    {
                        if (!socket.SendAsync(eventArg))
                        {
                            ProcessSend(eventArg, false);
                        }
                        result = true;
                    }
                }
            }

            return result;
        }

        public override void Update()
        {
            object obj = eventLock;
            Monitor.Enter(obj);
            try
            {
                Queue<SocketAsyncEventArgs> queue = appendEventArgs;
                appendEventArgs = processEventArgs;
                processEventArgs = queue;
            }
            finally
            {
                Monitor.Exit(obj);
            }

            while (processEventArgs.Count > 0)
            {
                SocketAsyncEventArgs socketAsyncEventArgs = processEventArgs.Dequeue();
                SocketAsyncOperation lastOperation = socketAsyncEventArgs.LastOperation;
                switch (lastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        ProcessConnect(socketAsyncEventArgs, true);
                        break;
                    case SocketAsyncOperation.Disconnect:
                        goto IL_89;
                    case SocketAsyncOperation.Receive:
                        ProcessReceive(socketAsyncEventArgs, true);
                        break;
                    default:
                        if (lastOperation != SocketAsyncOperation.Send)
                        {
                            goto IL_89;
                        }
                        ProcessSend(socketAsyncEventArgs, true);
                        break;
                }
                continue;
                IL_89:
                LoggerManager.Instance.Error("Invalid operation completed");
            }
        }

        private void SocketEventArgCompleted(object sender, SocketAsyncEventArgs e)
        {
            SocketAsyncOperation lastOperation = e.LastOperation;
            switch (lastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e, false);
                    return;
                case SocketAsyncOperation.Disconnect:
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e, false);
                    return;
                default:
                    if (lastOperation == SocketAsyncOperation.Send)
                    {
                        ProcessSend(e, false);
                        return;
                    }
                    break;
            }

            LoggerManager.Instance.Error("Invalid operation completed");
            throw new Exception("Invalid operation completed");
        }

        private void ProcessConnect(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                SocketError socketError = e.SocketError;
                PushAppendEventArg(e);
                if (socketError == SocketError.Success)
                {
                    SocketAsyncEventArgs eventArg = GetEventArg(true);
                    if (eventArg != null)
                    {
                        if (!socket.ReceiveAsync(eventArg))
                        {
                            ProcessReceive(eventArg, false);
                        }
                    }
                }
            }
            else
            {
                if (e.SocketError == SocketError.Success)
                {
                    LoggerManager.Instance.Debug("Successfully connected to the server {0}", e.RemoteEndPoint);

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

                    if (handlerPipeline.InHeader != null)
                    {
                        handlerPipeline.InHeader.OnConnected(this, e.SocketError);
                    }
                    PushFreeEventArg(e, true);
                }
                else
                {
                    LoggerManager.Instance.Error("Failed to connect to {0}, Error Code:{1}", remote.ToString(), e.SocketError);

                    if (handlerPipeline.InHeader != null)
                    {
                        handlerPipeline.InHeader.OnConnected(this, e.SocketError);
                    }
                    PushFreeEventArg(e, true);

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
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                PushAppendEventArg(e);
            }
            else
            {
                if (e.SocketError != SocketError.Success)
                {
                    object obj = statusLock;
                    Monitor.Enter(obj);
                    try
                    {
                        if (connectionStatus != ConnectionStatus.Disconnected)
                        {
                            try
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Disconnect(true);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Instance.Error(ex.ToString());
                            }
                            LoggerManager.Instance.Error("ProcessSend Disconnected from {0}, Error code:{1}", remote.ToString(), e.SocketError);

                            if (handlerPipeline.InHeader != null)
                            {
                                handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                            }
                            connectionStatus = ConnectionStatus.Disconnected;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(obj);
                    }
                }

                PushFreeEventArg(e, true);
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                PushAppendEventArg(e);

                SocketError socketError = e.SocketError;
                int bytesTransferred = e.BytesTransferred;
                if (socketError == SocketError.Success && bytesTransferred > 0)
                {
                    SocketAsyncEventArgs eventArg = GetEventArg(true);
                    if (eventArg == null)
                    {
                        try
                        {
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Disconnect(true);
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Instance.Error(ex.ToString());
                        }

                        LoggerManager.Instance.Error("There is no event to post for receive, so actively close the socket");
                        if (handlerPipeline.InHeader != null)
                        {
                            e.SocketError = SocketError.ConnectionAborted;
                        }
                    }
                    else
                    {
                        if (!socket.ReceiveAsync(eventArg))
                        {
                            ProcessReceive(eventArg, false);
                        }
                    }
                }
            }
            else
            {
                if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
                {
                    if (handlerPipeline.InHeader != null)
                    {
                        try
                        {
                            handlerPipeline.InHeader.OnReceived(this, e.Buffer, e.Offset, e.BytesTransferred);
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Instance.Error(ex.ToString());
                        }
                    }
                    PushFreeEventArg(e, false);
                }
                else
                {
                    object obj = statusLock;
                    Monitor.Enter(obj);
                    try
                    {
                        if (connectionStatus == ConnectionStatus.ClosedByLogic)
                        {
                            return;
                        }

                        if (connectionStatus != ConnectionStatus.Disconnected)
                        {
                            try
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Disconnect(true);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Instance.Error(ex.ToString());
                            }

                            if (e.SocketError != SocketError.Success)
                            {
                                LoggerManager.Instance.Error("ProcessReceive Disconnected from {0}, Error code:{1}", remote.ToString(), e.SocketError);
                            }

                            if (handlerPipeline.InHeader != null)
                            {
                                handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                            }
                            connectionStatus = ConnectionStatus.Disconnected;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(obj);
                    }

                    PushFreeEventArg(e, false);
                }
            }
        }

        public override long GetNetworkPing()
        {
            return -1L;
        }
        public override long GetAliasedPing()
        {
            return -1L;
        }

        public override bool GetSendStatics(out uint sendBytes, out uint sendNum, out long totalTime)
        {
            sendBytes = 0u;
            sendNum = 0u;
            totalTime = 0L;
            return false;
        }

        public override bool GetRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime)
        {
            recvBytes = 0u;
            recvNum = 0u;
            totalTime = 0L;
            return false;
        }

        public override void PauseStatics()
        {
        }

        public override void ResumeStatics()
        {
        }
    }
}
