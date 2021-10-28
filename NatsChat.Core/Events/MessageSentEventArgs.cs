using NatsChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatsChat.Core.Events
{
    public class MessageSentEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
