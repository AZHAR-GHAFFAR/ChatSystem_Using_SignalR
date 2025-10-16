using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }
    }
}
