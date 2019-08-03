using System;
using System.Net.Sockets;
using LywGames.Common;
using LywGames.Messages;

namespace LywGames.Network
{
    public class MessageInProcessor : AbstractNetworkInHandler
    {
        private AbstractMessageInitializer messageInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        public MessageInProcessor(AbstractMessageInitializer messageInitializer, MessageDelegateProcessor msgDelegateProcessor)
        {
            this.messageInitializer = messageInitializer;
            this.msgDelegateProcessor = msgDelegateProcessor;
        }
        public override void OnReceived(IConnection connection, byte[] buffer, int offset, int size)
        {
            NetworkBuffer networkBuffer = new NetworkBuffer(buffer, offset, size, true);
            int num = networkBuffer.ReadInt32();
            int callback = networkBuffer.ReadInt32();
            Type messageType = this.messageInitializer.getMessageType(num);
            if (null == messageType)
            {
                LoggerManager.Instance.Warn("OnReceived found msg protocolId {0}-{0:X} can't getMessageType then ingored", new object[]
                {
                    num
                });
            }
            else
            {
                Message message = (Message)Activator.CreateInstance(messageType);
                message.Callback = callback;
                message.DecodeBody(buffer, offset + networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
                if (num == 131075)
                {
                    GCLoginGameMessage gC_LoginGameMessage = (GCLoginGameMessage)message;
                    connection.LoginGameRes = gC_LoginGameMessage;
                    LoggerManager.Instance.Info("connection {0} recv loginRes callback {1}", new object[]
                    {
                        connection.GetHashCode(),
                        gC_LoginGameMessage.Callback
                    });
                }
                else
                {
                    connection.recvProtocol(num);
                    IMessageHandler messageHandler = this.messageInitializer.GetMessageHandler(num);
                    if (messageHandler != null)
                    {
                        messageHandler.handleMessage(connection, message);
                    }
                    else
                    {
                        if (!this.msgDelegateProcessor.HandleMessage(message, connection))
                        {
                            messageHandler = this.messageInitializer.GetDefaultMessageHandler();
                            if (messageHandler != null)
                            {
                                messageHandler.handleMessage(connection, message);
                            }
                        }
                    }
                }
            }
        }
        public override void OnReceived(IConnection connection, object msg)
        {
        }
        public override void OnRequestTimeout(IConnection connection, int userData)
        {
            IMessageHandler messageHandler = this.messageInitializer.GetMessageHandler(userData);
            if (messageHandler != null)
            {
                messageHandler.handleRequestTimeout(connection, userData);
            }
            base.OnRequestTimeout(connection, userData);
        }
        public override void OnConnected(IConnection connection, SocketError result)
        {
            base.FireConnected(connection, result);
            if (this.messageInitializer.GetConnectionActiveHandler() != null)
            {
                this.messageInitializer.GetConnectionActiveHandler().handleConnectionActive(connection, result);
            }
        }
        public override void OnDisconnected(IConnection connection, SocketError error)
        {
            if (this.messageInitializer.GetConnectionInactiveHandler() != null)
            {
                this.messageInitializer.GetConnectionInactiveHandler().handleConnectionActive(connection, error);
            }
        }
    }
}
