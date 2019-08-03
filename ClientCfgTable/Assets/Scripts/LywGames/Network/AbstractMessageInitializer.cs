using System;
using System.Collections.Generic;
using LywGames.Messages;

namespace LywGames.Network
{
    public abstract class AbstractMessageInitializer
    {
        private Dictionary<int, IMessageHandler> msgHandlers = new Dictionary<int, IMessageHandler>();
        private Dictionary<int, Type> msgs = new Dictionary<int, Type>();
        private IMessageHandler defaultMsgHandler;
        private IMessageHandler connectionActiveHandler;
        private IMessageHandler connectionInactiveHandler;
        public AbstractMessageInitializer()
        {
            this.Initilial();
        }
        public abstract void Initilial();
        public Type getMessageType(int protocolId)
        {
            Type result = null;
            this.msgs.TryGetValue(protocolId, out result);
            return result;
        }
        public IMessageHandler GetMessageHandler(int protocolId)
        {
            IMessageHandler result = null;
            this.msgHandlers.TryGetValue(protocolId, out result);
            return result;
        }
        public IMessageHandler GetConnectionActiveHandler()
        {
            return this.connectionActiveHandler;
        }
        public IMessageHandler GetConnectionInactiveHandler()
        {
            return this.connectionInactiveHandler;
        }
        public IMessageHandler GetDefaultMessageHandler()
        {
            return this.defaultMsgHandler;
        }
        public void AddMessageHanlder(Type msg, IMessageHandler handler)
        {
            this.msgHandlers.Add(Message.GetProtocolId(msg), handler);
        }
        public void AddMessage(Type msg)
        {
            this.msgs.Add(Message.GetProtocolId(msg), msg);
        }
        public void RemoveMessage(Type msg)
        {
            this.msgs.Remove(Message.GetProtocolId(msg));
        }
        public void SetConnectionActiveHandler(IMessageHandler handler)
        {
            this.connectionActiveHandler = handler;
        }
        public void SetConnectionInactiveHandler(IMessageHandler handler)
        {
            this.connectionInactiveHandler = handler;
        }
        public void SetDefaultMsgHandler(IMessageHandler handler)
        {
            this.defaultMsgHandler = handler;
        }
    }
}
