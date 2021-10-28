using NatsChat.Core;
using NatsChat.Core.Events;
using NatsChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NatsChat.ConsoleApp
{
    public class ConsoleChatService : IMessageSubscriber, IDisposable
    {
        private readonly ChatSettings _chatSettings;
        private readonly NatsService _natsService;

        public List<Message> Conversation { get; set; }

        public ConsoleChatService(ChatSettings chatSettings)
        {
            _chatSettings = chatSettings;

            _natsService = new NatsService(_chatSettings);
            Subscribe(_natsService);

            _natsService.SubscribeToSubject(_chatSettings.Subject);

            Conversation = new List<Message>();
        }

        public void InitChat(string username)
        {
            InitSendLoop(username);
        }

        public void InitSendLoop(string username)
        {
            string input = string.Empty;
            Console.Clear();
            Console.WriteLine("(Enter \"/q\" to exit)\n");
            Console.Write("Your message: ");
            while (input != "/q")
            {
                input = Console.ReadLine();

                if (input.Equals("/q", StringComparison.OrdinalIgnoreCase))
                    continue;

                _natsService.PublishMessage(_chatSettings.Subject, new Message
                {
                    Id = username,
                    Sent = DateTime.Now,
                    MessageText = input
                });
            }
        }

        public void HandleIncomingChatMessage(object sender, MessageReceivedEventArgs e)
        {
            Conversation.Add(e.Message);

            Console.Clear();
            PrintConversation();
            Console.Write("Your message: ");
        }

        public void Subscribe(IMessagePublisher publisher)
        {
            publisher.OnMessageReceived += HandleIncomingChatMessage;
        }

        public void Dispose()
        {
            _natsService.Dispose();
        }

        private void PrintConversation()
        {
            Console.WriteLine("(Enter \"/q\" to exit)\n");
            if (Conversation.Count > 10)
            {
                Console.WriteLine("...");
            }

            // Only display the 10 latest messages, ordered by date and time sent, ascending
            var latestMessages = Conversation
                .OrderByDescending(m => m.Sent)
                .Take(10)
                .OrderBy(m => m.Sent);

            foreach (var message in latestMessages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
