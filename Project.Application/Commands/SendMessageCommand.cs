using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Entities;
using Project.Infrastructure;

namespace Project.Application.Commands
{
    public class SendMessageCommand : IRequest<Message>
    {
        public string Username { get; set; }
        public string? Content { get; set; }
        public IFormFile? File { get; set; }
    }

    public class SendMessageHandler : IRequestHandler<SendMessageCommand, Message>
    {
        private readonly IChatRepository _repository;

        public SendMessageHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Message> Handle(SendMessageCommand request, CancellationToken ct)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Content = request.Content,
                Timestamp = DateTime.UtcNow
            };

            if (request.File != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                message.FileUrl = $"/uploads/{fileName}";
                message.FileName = request.File.FileName;

                Console.WriteLine($"✅ File saved at: {filePath}");
            }


            await _repository.AddMessageAsync(message);
            return message;
        }
    }
}

