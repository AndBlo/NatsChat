using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client;
using NatsChat.Core.Events;
using NatsChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NatsChat.Core
{
    public class NatsService : IMessagePublisher, IDisposable
    {
        private readonly IEncodedConnection _connection;
        private readonly ILogger<NatsService> _logger;
        private readonly ChatSettings _chatSettings;

        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        public NatsService(IOptions<ChatSettings> options, ILogger<NatsService> logger)
        {
            _logger = logger;
            _chatSettings = options.Value;

            _connection = SetupConnection();
        }

        public NatsService(ChatSettings chatSettings)
        {
            _chatSettings = chatSettings;

            _connection = SetupConnection();
        }

        public IEncodedConnection SetupConnection()
        {
            try
            {
                var connection = new ConnectionFactory()
                        .CreateEncodedConnection(_chatSettings.NatsUrl);

                connection.OnSerialize = Serialize;
                connection.OnDeserialize = Deserialize;
                return connection;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Could not connect to NATS server.\nUrl: {_chatSettings.NatsUrl}");

                throw;
            }
        }

        public void SubscribeToSubject(string subject)
        {
            _connection.SubscribeAsync(subject, (sender, args) =>
            {
                var receivedMessage = (Message)args.ReceivedObject;

                ProcessIncomingMessage(receivedMessage);
            });
        }

        public void PublishMessage(string subject, Message message)
        {
            _connection.Publish(subject, message);
        }

        public void ProcessIncomingMessage(Message message)
        {
            if (OnMessageReceived != null)
            {
                OnMessageReceived(this, new MessageReceivedEventArgs() { Message = message });
            }
        }

        private byte[] Serialize(object message)
        {
            return JsonSerializer.SerializeToUtf8Bytes((Message)message);
        }

        private object Deserialize(byte[] messageBytes)
        {
            return JsonSerializer.Deserialize<Message>(messageBytes);
        }

        private void LogError(Exception exception, string message = null)
        {
            if(_logger != null)
            {
                _logger.LogError(exception, message);
            }
            else
            {
                Console.WriteLine(string.IsNullOrEmpty(message) ? exception.Message : message);
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
