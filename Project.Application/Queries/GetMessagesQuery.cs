using MediatR;
using Project.Domain.Entities;
using Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Queries
{
    public class GetMessagesQuery : IRequest<List<Message>>
    {
        public int Count { get; set; } = 50;
    }

    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<Message>>
    {
        private readonly IChatRepository _repository;

        public GetMessagesHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Message>> Handle(GetMessagesQuery request, CancellationToken ct)
        {
            return await _repository.GetMessagesAsync(request.Count);
        }
    }
}
