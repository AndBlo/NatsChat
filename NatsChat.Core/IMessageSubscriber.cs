using NatsChat.Core.Events;

namespace NatsChat.Core
{
    public interface IMessageSubscriber
    {
        void HandleIncomingChatMessage(object sender, MessageReceivedEventArgs e);
        void Subscribe(IMessagePublisher publisher);
    }
}
