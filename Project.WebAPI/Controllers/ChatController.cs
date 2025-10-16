using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Application.Commands;
using Project.Application.Queries;
using Project.Domain.Entities;
using Project.Infrastructure;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("messages")]
        public async Task<ActionResult<List<Message>>> GetMessages([FromQuery] int count = 50)
        {
            var query = new GetMessagesQuery { Count = count };
            var messages = await _mediator.Send(query);
            return Ok(messages);
        }

        [HttpPost("messages")]
        public async Task<ActionResult<Message>> SendMessage(
    [FromForm] SendMessageRequest request,
    [FromServices] IHubContext<ChatHub> hubContext)
        {
            var command = new SendMessageCommand
            {
                Username = request.Username,
                Content = request.Content,
                File = request.File
            };

            var message = await _mediator.Send(command);

            // ✅ broadcast message with file info
            await hubContext.Clients.All.SendAsync("ReceiveMessage", message);

            return Ok(message);
        }
    }

    public class SendMessageRequest
        {
            public string Username { get; set; }
            public string? Content { get; set; }
            public IFormFile? File { get; set; }
        }
    }
