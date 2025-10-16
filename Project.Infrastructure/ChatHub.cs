using MediatR;
using Microsoft.AspNetCore.SignalR;
using Project.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(string username, string? message)
        {
            var command = new SendMessageCommand
            {
                Username = username,
                Content = message
            };

            var result = await _mediator.Send(command);

            // Send to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", result);
        }

        public async Task SendTyping(string username)
        {
            await Clients.Others.SendAsync("UserTyping", username);
        }
    }
}
