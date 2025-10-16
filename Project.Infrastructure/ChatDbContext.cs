using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;


namespace Project.Infrastructure
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }
    }
}
