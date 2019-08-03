using LywGames.Messages;

namespace LywGames.Network
{
    public class MessageDelegateProcessor
    {
        private MessageDelegateInitializer delegateInitializer;

        public MessageDelegateProcessor(MessageDelegateInitializer delegateInitializer)
        {
            this.delegateInitializer = delegateInitializer;
        }

        public bool HandleMessage(Message msg, IConnection connection)
        {
            MessageDelegateNode messageDelegate = delegateInitializer.getMessageDelegate(msg.GetType());

            bool result;
            if (messageDelegate == null)
            {
                result = false;
            }
            else
            {
                messageDelegate.ReceiveMessage(msg);
                if (messageDelegate.isShortConnect)
                {
                    connection.Disconnect();
                }
                result = true;
            }

            return result;
        }

    }
}
