using LywGames.Messages;

namespace LywGames.Network
{
    public class MessageDelegateProcessor
    {
        private MessageDelegateInitializer messageDelegateInitializer;

        public MessageDelegateProcessor(MessageDelegateInitializer messageDelegateInitializer)
        {
            this.messageDelegateInitializer = messageDelegateInitializer;
        }

        public bool HandleMessage(Message msg, IConnection connection)
        {
            MessageDelegateNode messageDelegateNode = messageDelegateInitializer.getMessageDelegate(msg.GetType());

            bool result;
            if (messageDelegateNode == null)
            {
                result = false;
            }
            else
            {
                messageDelegateNode.ReceiveMessage(msg);
                if (messageDelegateNode.isShortConnect)
                {
                    connection.Disconnect();
                }
                result = true;
            }

            return result;
        }

    }
}
