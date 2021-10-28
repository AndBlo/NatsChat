using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatsChat.Core.Models
{
    public class ChatSettings
    {
        public string NatsUrl { get; set; }
        public string Subject { get; set; }
    }
}
