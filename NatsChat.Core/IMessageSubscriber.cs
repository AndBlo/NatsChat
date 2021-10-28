using NatsChat.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatsChat.Core
{
    public interface IMessageSubscriber
    {
        void HandleIncomingChatMessage(object sender, MessageReceivedEventArgs e);
        void Subscribe(IMessagePublisher publisher);
    }
}
