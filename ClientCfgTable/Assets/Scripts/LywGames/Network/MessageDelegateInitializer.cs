using System;
using System.Collections.Generic;

namespace LywGames.Network
{
    public class MessageDelegateInitializer
    {
        private Dictionary<Type, MessageDelegateNode> messageType2MessageDelegateNodeDic = new Dictionary<Type, MessageDelegateNode>();

        public void AddMessageReceiveDelegate(Type msgType, MessageDelegateNode msgDelegate)
        {
            messageType2MessageDelegateNodeDic.Add(msgType, msgDelegate);
        }

        public MessageDelegateNode GetMessageDelegate(Type msgType)
        {
            return messageType2MessageDelegateNodeDic[msgType];
        }

    }
}
