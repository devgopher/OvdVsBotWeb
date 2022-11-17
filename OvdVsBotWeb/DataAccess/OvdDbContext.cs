using Microsoft.EntityFrameworkCore;
using OvdVsBotWeb.Models.Data;

namespace OvdVsBotWeb.DataAccess
{
    public class OvdDbContext : DbContext
    {
        public OvdDbContext(DbContextOptions options) : base(options)
        {           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>();
        }

        public DbSet<Chat> Chats { get; set; }
    }
}
