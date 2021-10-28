using Microsoft.AspNetCore.Mvc;
using NatsChat.Core.Models;
using NatsChat.WebApi.Services;
using System.Collections.Generic;

namespace NatsChat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("messages")]
        public IEnumerable<Message> GetMessages()
        {
            return _chatService.GetMessages();
        }

        [HttpPost("messages")]
        public void PostMessage([FromBody]Message message)
        {
            _chatService.SendMessage(message);
        }
    }
}
