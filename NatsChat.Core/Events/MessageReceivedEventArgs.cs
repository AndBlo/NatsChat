using NatsChat.Core.Models;
using System;

namespace NatsChat.Core.Events
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
