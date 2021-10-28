using NatsChat.Core.Events;
using NatsChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatsChat.Core
{
    public interface IMessagePublisher
    {
        event EventHandler<MessageReceivedEventArgs> OnMessageReceived;
        void ProcessIncomingMessage(Message chatMessage);
    }
}
