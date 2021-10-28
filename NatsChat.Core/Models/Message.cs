using System;

namespace NatsChat.Core.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string MessageText { get; set; }
        public DateTime Sent { get; set; }

        public override string ToString()
        {
            return $"{Sent} {Id}: {MessageText}";
        }
    }
}
