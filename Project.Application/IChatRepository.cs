using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
        public interface IChatRepository
        {
            Task AddMessageAsync(Message message);
            Task<List<Message>> GetMessagesAsync(int count);
        }
}
