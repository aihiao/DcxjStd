using System;
using LywGames.Messages;

namespace LywGames.Network
{
    public class MessageDelegateNode
    {
        public Action<Message> receiveAction;
        public bool isShortConnect = false;
        public void ReceiveMessage(Message message)
        {
            receiveAction(message);
        }
    }
}