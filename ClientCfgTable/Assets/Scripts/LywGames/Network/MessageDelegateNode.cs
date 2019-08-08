using System;
using LywGames.Messages;

namespace LywGames.Network
{
    public class MessageDelegateNode
    {
        public bool isShortConnect = false; // 如果是短连接, 接收完消息, 断开连接。

        public Action<Message> receiveAction;
        public void ReceiveMessage(Message message)
        {
            receiveAction(message);
        }

    }
}