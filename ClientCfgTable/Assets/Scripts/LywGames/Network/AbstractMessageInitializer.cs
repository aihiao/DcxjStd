using System;
using System.Collections.Generic;
using LywGames.Messages;

namespace LywGames.Network
{
    public abstract class AbstractMessageInitializer
    {
        private Dictionary<int, IMessageHandler> protocolId2MessageHandlerDic = new Dictionary<int, IMessageHandler>();
        private Dictionary<int, Type> protocolId2MessageTypeDic = new Dictionary<int, Type>();

        private IMessageHandler defaultMsgHandler;
        private IMessageHandler connectionActiveHandler;
        private IMessageHandler connectionInactiveHandler;

        public AbstractMessageInitializer()
        {
            Initilial();
        }

        public abstract void Initilial();

        public Type getMessageType(int protocolId)
        {
            return protocolId2MessageTypeDic[protocolId];
        }

        public IMessageHandler GetMessageHandler(int protocolId)
        {
            return protocolId2MessageHandlerDic[protocolId];
        }

        public void AddMessageHanlder(Type msg, IMessageHandler handler)
        {
            protocolId2MessageHandlerDic.Add(Message.GetProtocolId(msg), handler);
        }

        public void AddMessageType(Type msg)
        {
            protocolId2MessageTypeDic.Add(Message.GetProtocolId(msg), msg);
        }

        public void RemoveMessageType(Type msg)
        {
            protocolId2MessageTypeDic.Remove(Message.GetProtocolId(msg));
        }

        public IMessageHandler GetDefaultMessageHandler()
        {
            return defaultMsgHandler;
        }

        public void SetDefaultMsgHandler(IMessageHandler handler)
        {
            defaultMsgHandler = handler;
        }

        public IMessageHandler GetConnectionActiveHandler()
        {
            return connectionActiveHandler;
        }

        public void SetConnectionActiveHandler(IMessageHandler handler)
        {
            connectionActiveHandler = handler;
        }

        public IMessageHandler GetConnectionInactiveHandler()
        {
            return connectionInactiveHandler;
        }

        public void SetConnectionInactiveHandler(IMessageHandler handler)
        {
            connectionInactiveHandler = handler;
        }

    }
}
