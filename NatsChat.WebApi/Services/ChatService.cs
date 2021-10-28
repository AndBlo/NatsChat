using Microsoft.Extensions.Options;
using NatsChat.Core;
using NatsChat.Core.Events;
using NatsChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NatsChat.WebApi.Services
{
    public class ChatService : IMessageSubscriber, IDisposable
    {
        private readonly NatsService _natsService;
        private readonly List<Message> _conversation;
        private readonly string _subject;

        public ChatService(NatsService natsService, IOptions<ChatSettings> chatOptions)
        {
            _natsService = natsService;
            Subscribe(_natsService);
            _conversation = new List<Message>();
            _subject = chatOptions.Value.Subject;
            _natsService.SubscribeToSubject(_subject);
        }

        public void SendMessage(Message message)
        {
            message.Sent = DateTime.Now;

            _natsService.PublishMessage(_subject, message);
        }

        public List<Message> GetMessages()
        {
            return _conversation.OrderBy(m => m.Sent).ToList();
        }

        public void HandleIncomingChatMessage(object sender, MessageReceivedEventArgs e)
        {
            _conversation.Add(e.Message);
        }

        public void Subscribe(IMessagePublisher publisher)
        {
            publisher.OnMessageReceived += HandleIncomingChatMessage;
        }

        public void Dispose()
        {
            _natsService.Dispose();
        }
    }
}
