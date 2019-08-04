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
        public static int _MAX_EVENT_NUMBER = 128;
        private Socket socket;
        private BufferManager bufferManager;
        private int evenCount;
        private object eventLock = new object();
        private Queue<SocketAsyncEventArgs> freeEventArgs = new Queue<SocketAsyncEventArgs>();
        private Queue<SocketAsyncEventArgs> processEventArgs = new Queue<SocketAsyncEventArgs>();
        private Queue<SocketAsyncEventArgs> appendEventArgs = new Queue<SocketAsyncEventArgs>();

        public TCPConnection(int userData)
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.NoDelay = true;
            this.evenCount = 0;
            int num = _MAX_EVENT_NUMBER / 2;
            this.bufferManager = new BufferManager(num * NetworkParameters._MAX_RECV_BUFFER_SIZE, NetworkParameters._MAX_RECV_BUFFER_SIZE);
            this.bufferManager.InitBuffer();
        }

        private SocketAsyncEventArgs GetEventArg(bool needBuffer)
        {
            object obj;
            Monitor.Enter(obj = this.eventLock);
            SocketAsyncEventArgs result;
            try
            {
                if (this.freeEventArgs.Count > 0)
                {
                    SocketAsyncEventArgs socketAsyncEventArgs = this.freeEventArgs.Dequeue();
                    if (needBuffer && socketAsyncEventArgs.Buffer == null)
                    {
                        if (!this.bufferManager.SetBuffer(socketAsyncEventArgs))
                        {
                            result = null;
                            return result;
                        }
                    }
                    else
                    {
                        if (!needBuffer && socketAsyncEventArgs.Buffer != null)
                        {
                            this.bufferManager.FreeBuffer(socketAsyncEventArgs);
                        }
                    }
                    result = socketAsyncEventArgs;
                }
                else
                {
                    if (this.evenCount < TCPConnection._MAX_EVENT_NUMBER)
                    {
                        this.evenCount++;
                        SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
                        socketAsyncEventArgs.RemoteEndPoint = this.remote;
                        socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(this.SocketEventArg_Completed);
                        if (needBuffer)
                        {
                            if (!this.bufferManager.SetBuffer(socketAsyncEventArgs))
                            {
                                result = null;
                                return result;
                            }
                        }
                        result = socketAsyncEventArgs;
                    }
                    else
                    {
                        LoggerManager.Instance.Warn("GetEventArd found without free event, MaxEventCount {0}", new object[]
                        {
                            TCPConnection._MAX_EVENT_NUMBER
                        });
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
            object obj;
            Monitor.Enter(obj = this.eventLock);
            try
            {
                if (clearBuffer)
                {
                    if (eventArg.Buffer != null)
                    {
                        this.bufferManager.FreeBuffer(eventArg);
                    }
                    else
                    {
                        eventArg.SetBuffer(null, 0, 0);
                    }
                }
                this.freeEventArgs.Enqueue(eventArg);
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        private void PushAppendEventArg(SocketAsyncEventArgs eventArg)
        {
            object obj;
            Monitor.Enter(obj = this.eventLock);
            try
            {
                this.appendEventArgs.Enqueue(eventArg);
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        public override void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress)
        {
            LoggerManager.Instance.Debug("TCPConnection.connectAsync remote {0}", new object[]
            {
                remoteAddress
            });
            this.remote = remoteAddress;
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
            SocketAsyncEventArgs eventArg = this.GetEventArg(false);
            if (eventArg == null)
            {
                LoggerManager.Instance.Error("Can't eventArg when connecting", new object[0]);
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
            else
            {
                if (!this.socket.ConnectAsync(eventArg))
                {
                    this.ProcessConnect(eventArg, false);
                }
            }
        }

        public override bool ConnectSync(IPEndPoint localAddress, IPEndPoint remoteAddress, int timeout)
        {
            long num = DateTime.Now.ToFileTime() / 10000L;
            this.remote = remoteAddress;
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
            SocketAsyncEventArgs eventArg = this.GetEventArg(false);
            bool result;
            if (eventArg == null)
            {
                LoggerManager.Instance.Error("Can't eventArg when connecting", new object[0]);
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
            }
            else
            {
                if (!this.socket.ConnectAsync(eventArg))
                {
                    this.ProcessConnect(eventArg, false);
                }
                long num2 = DateTime.Now.ToFileTime() / 10000L;
                while (num2 - num < (long)(timeout * 1000))
                {
                    if (this.connectionStatus == IConnection.ConnectionStatus.CONNECTIONSTATUS_CONNECTED)
                    {
                        result = true;
                        return result;
                    }
                    Thread.Sleep(100);
                    num2 = DateTime.Now.ToFileTime() / 10000L;
                }
                result = false;
            }
            return result;
        }

        public override void Disconnect()
        {
            if (this.socket != null)
            {
                try
                {
                    LoggerManager.Instance.Debug("Logic call Disconnect {0}", new object[]
                    {
                        this.socket.RemoteEndPoint
                    });
                    this.socket.Shutdown(SocketShutdown.Both);
                    this.socket.Disconnect(true);
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
                }
                catch (Exception var_0_75)
                {
                }
            }
        }

        internal override bool _Send(byte[] buffer, int offset, int count)
        {
            bool result;
            if (this.socket == null || !this.socket.Connected)
            {
                LoggerManager.Instance.Warn("TCPConnection._Send data len {0} but socket is not connected", new object[]
                {
                    count
                });
                result = false;
            }
            else
            {
                SocketAsyncEventArgs eventArg = this.GetEventArg(false);
                if (eventArg == null)
                {
                    LoggerManager.Instance.Error("There is no SocketAsyncEventArgs available", new object[0]);
                    result = false;
                }
                else
                {
                    if (!this.bufferManager.SetBuffer(eventArg, buffer, offset, count))
                    {
                        LoggerManager.Instance.Error("There is no memory available in buffermanager", new object[0]);
                        result = false;
                    }
                    else
                    {
                        if (!this.socket.SendAsync(eventArg))
                        {
                            this.ProcessSend(eventArg, false);
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        public override void Update()
        {
            object obj;
            Monitor.Enter(obj = this.eventLock);
            try
            {
                Queue<SocketAsyncEventArgs> queue = this.appendEventArgs;
                this.appendEventArgs = this.processEventArgs;
                this.processEventArgs = queue;
            }
            finally
            {
                Monitor.Exit(obj);
            }
            while (this.processEventArgs.Count > 0)
            {
                SocketAsyncEventArgs socketAsyncEventArgs = this.processEventArgs.Dequeue();
                SocketAsyncOperation lastOperation = socketAsyncEventArgs.LastOperation;
                switch (lastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        this.ProcessConnect(socketAsyncEventArgs, true);
                        break;
                    case SocketAsyncOperation.Disconnect:
                        goto IL_89;
                    case SocketAsyncOperation.Receive:
                        this.ProcessReceive(socketAsyncEventArgs, true);
                        break;
                    default:
                        if (lastOperation != SocketAsyncOperation.Send)
                        {
                            goto IL_89;
                        }
                        this.ProcessSend(socketAsyncEventArgs, true);
                        break;
                }
                continue;
                IL_89:
                LoggerManager.Instance.Error("Invalid operation completed", new object[0]);
            }
        }

        private void SocketEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            SocketAsyncOperation lastOperation = e.LastOperation;
            switch (lastOperation)
            {
                case SocketAsyncOperation.Connect:
                    this.ProcessConnect(e, false);
                    return;
                case SocketAsyncOperation.Disconnect:
                    break;
                case SocketAsyncOperation.Receive:
                    this.ProcessReceive(e, false);
                    return;
                default:
                    if (lastOperation == SocketAsyncOperation.Send)
                    {
                        this.ProcessSend(e, false);
                        return;
                    }
                    break;
            }
            LoggerManager.Instance.Error("Invalid operation completed", new object[0]);
            throw new Exception("Invalid operation completed");
        }

        private void ProcessConnect(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                SocketError socketError = e.SocketError;
                this.PushAppendEventArg(e);
                if (socketError == SocketError.Success)
                {
                    SocketAsyncEventArgs eventArg = this.GetEventArg(true);
                    if (eventArg != null)
                    {
                        if (!this.socket.ReceiveAsync(eventArg))
                        {
                            this.ProcessReceive(eventArg, false);
                        }
                    }
                }
            }
            else
            {
                if (e.SocketError == SocketError.Success)
                {
                    LoggerManager.Instance.Debug("Successfully connected to the server {0}", new object[]
                    {
                        e.RemoteEndPoint
                    });
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
                    if (this.handlerPipeline.InHeader != null)
                    {
                        this.handlerPipeline.InHeader.OnConnected(this, e.SocketError);
                    }
                    this.PushFreeEventArg(e, true);
                }
                else
                {
                    LoggerManager.Instance.Error("Failed to connect to {0}, Error Code:{1}", new object[]
                    {
                        this.remote.ToString(),
                        e.SocketError
                    });
                    if (this.handlerPipeline.InHeader != null)
                    {
                        this.handlerPipeline.InHeader.OnConnected(this, e.SocketError);
                    }
                    this.PushFreeEventArg(e, true);
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
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                SocketError socketError = e.SocketError;
                int bytesTransferred = e.BytesTransferred;
                this.PushAppendEventArg(e);
                if (socketError == SocketError.Success && bytesTransferred > 0)
                {
                    SocketAsyncEventArgs eventArg = this.GetEventArg(true);
                    if (eventArg == null)
                    {
                        try
                        {
                            this.socket.Shutdown(SocketShutdown.Both);
                            this.socket.Disconnect(true);
                        }
                        catch (Exception)
                        {
                        }
                        LoggerManager.Instance.Error("There is no event to post for receive, so actively close the socket", new object[0]);
                        if (this.handlerPipeline.InHeader != null)
                        {
                            e.SocketError = SocketError.ConnectionAborted;
                        }
                    }
                    else
                    {
                        if (!this.socket.ReceiveAsync(eventArg))
                        {
                            this.ProcessReceive(eventArg, false);
                        }
                    }
                }
            }
            else
            {
                if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
                {
                    if (this.handlerPipeline.InHeader != null)
                    {
                        try
                        {
                            this.handlerPipeline.InHeader.OnReceived(this, e.Buffer, e.Offset, e.BytesTransferred);
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Instance.Error("ProcessReceive " + ex.ToString(), new object[0]);
                        }
                    }
                    this.PushFreeEventArg(e, false);
                }
                else
                {
                    object statusLock;
                    Monitor.Enter(statusLock = this.statusLock);
                    try
                    {
                        if (this.connectionStatus == IConnection.ConnectionStatus.CONNECTIONSTATUS_CLOSEDBYLOGIC)
                        {
                            return;
                        }
                        if (this.connectionStatus != IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED)
                        {
                            try
                            {
                                this.socket.Shutdown(SocketShutdown.Both);
                                this.socket.Disconnect(true);
                            }
                            catch (Exception)
                            {
                            }
                            if (e.SocketError != SocketError.Success)
                            {
                                LoggerManager.Instance.Error("ProcessReceive Disconnected from {0}, Error code:{1}", new object[]
                                {
                                    this.remote.ToString(),
                                    e.SocketError
                                });
                            }
                            if (this.handlerPipeline.InHeader != null)
                            {
                                this.handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                            }
                            this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(statusLock);
                    }
                    this.PushFreeEventArg(e, false);
                }
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                this.PushAppendEventArg(e);
            }
            else
            {
                if (e.SocketError != SocketError.Success)
                {
                    object statusLock;
                    Monitor.Enter(statusLock = this.statusLock);
                    try
                    {
                        if (this.connectionStatus != IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED)
                        {
                            try
                            {
                                this.socket.Shutdown(SocketShutdown.Both);
                                this.socket.Disconnect(true);
                            }
                            catch (Exception)
                            {
                            }
                            LoggerManager.Instance.Error("ProcessSend Disconnected from {0}, Error code:{1}", new object[]
                            {
                                this.remote.ToString(),
                                e.SocketError
                            });
                            if (this.handlerPipeline.InHeader != null)
                            {
                                this.handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                            }
                            this.connectionStatus = IConnection.ConnectionStatus.CONNECTIONSTATUS_DISCONNECTED;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(statusLock);
                    }
                }
                this.PushFreeEventArg(e, true);
            }
        }

        public override long getNetworkPing()
        {
            return -1L;
        }
        public override long getAliasedPing()
        {
            return -1L;
        }

        public override bool getSendStatics(out uint sendBytes, out uint sendNum, out long totalTime)
        {
            sendBytes = 0u;
            sendNum = 0u;
            totalTime = 0L;
            return false;
        }

        public override bool getRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime)
        {
            recvBytes = 0u;
            recvNum = 0u;
            totalTime = 0L;
            return false;
        }

        public override void pauseStatics()
        {
        }

        public override void resumeStatics()
        {
        }
    }
}
