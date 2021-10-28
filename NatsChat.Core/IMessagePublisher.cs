using NatsChat.Core.Events;
using NatsChat.Core.Models;
using System;

namespace NatsChat.Core
{
    public interface IMessagePublisher
    {
        event EventHandler<MessageReceivedEventArgs> OnMessageReceived;
        void ProcessIncomingMessage(Message chatMessage);
    }
}
