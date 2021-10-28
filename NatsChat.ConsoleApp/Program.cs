using NatsChat.Core.Models;
using System;
using System.Collections.Generic;

namespace NatsChat.ConsoleApp
{
    class Program
    {
        public static List<Message> Conversation { get; set; } = new List<Message>();
        public static bool Adding = false;

        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Please provide arguments for username (args[0]), subject (args[1]) and optional nats url (args[2])");

            string username = string.Empty;
            string subject = string.Empty;
            string url = null;

            if(args.Length >= 2)
            {
                username = args[0];
                subject = args[1];
                url = args.Length == 3 ? args[2] : null;
            }

            var chatSettings = new ChatSettings
            {
                Subject = subject,
                NatsUrl = url
            };

            using (var chatService = new ConsoleChatService(chatSettings))
            {
                chatService.InitChat(username);
            }
        }
    }
}
