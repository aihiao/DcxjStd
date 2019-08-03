using System;
using System.Collections.Generic;

namespace LywGames.Network
{
    public class MessageDelegateInitializer
    {
        private Dictionary<Type, MessageDelegateNode> msgReceiveDelegates = new Dictionary<Type, MessageDelegateNode>();

        public void AddMessageReceiveDelegate(Type msgType, MessageDelegateNode msgDelegate)
        {
            this.msgReceiveDelegates.Add(msgType, msgDelegate);
        }

        public MessageDelegateNode getMessageDelegate(Type msgType)
        {
            MessageDelegateNode result = null;
            msgReceiveDelegates.TryGetValue(msgType, out result);
            return result;
        }

    }
}
