using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
