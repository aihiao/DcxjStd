using System;
using System.Net.Sockets;
using LywGames.Common;
using LywGames.Messages;
using LywGames.Corgi.Protocol;

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
            int protocolId = networkBuffer.ReadInt32();
            int callBackId = networkBuffer.ReadInt32();
            Type messageType = messageInitializer.GetMessageType(protocolId);
            if (null == messageType)
            {
                LoggerManager.Instance.Warn("OnReceived found msg protocolId {0}-{0:X} can't getMessageType then ingored", protocolId);
            }
            else
            {
                Message message = (Message)Activator.CreateInstance(messageType);
                message.CallBackId = callBackId;
                message.DecodeBody(buffer, offset + networkBuffer.ReadOffset, networkBuffer.ReadableBytes);
                if (protocolId == Protocols.P_GC_GameLogin)
                {
                    GCLoginGameMessage gcLoginGameMessage = (GCLoginGameMessage)message;
                    connection.LoginGameRes = gcLoginGameMessage;
                    LoggerManager.Instance.Info("connection {0} recv loginRes callBackId {1}", connection.GetHashCode(), gcLoginGameMessage.CallBackId);
                }
                else
                {
                    connection.RecvProtocol(protocolId);
                    IMessageHandler messageHandler = messageInitializer.GetMessageHandler(protocolId);
                    if (messageHandler != null)
                    {
                        messageHandler.HandleMessage(connection, message);
                    }
                    else
                    {
                        if (!msgDelegateProcessor.HandleMessage(message, connection))
                        {
                            messageHandler = messageInitializer.GetDefaultMessageHandler();
                            if (messageHandler != null)
                            {
                                messageHandler.HandleMessage(connection, message);
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
            IMessageHandler messageHandler = messageInitializer.GetMessageHandler(userData);
            if (messageHandler != null)
            {
                messageHandler.HandleRequestTimeout(connection, userData);
            }
            OnRequestTimeout(connection, userData);
        }

        public override void OnConnected(IConnection connection, SocketError result)
        {
            FireConnected(connection, result);
            if (messageInitializer.GetConnectionActiveHandler() != null)
            {
                messageInitializer.GetConnectionActiveHandler().HandleConnectionActive(connection, result);
            }
        }

        public override void OnDisconnected(IConnection connection, SocketError error)
        {
            if (messageInitializer.GetConnectionInactiveHandler() != null)
            {
                messageInitializer.GetConnectionInactiveHandler().HandleConnectionActive(connection, error);
            }
        }

    }
}
