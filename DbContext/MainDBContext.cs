
using AmitalBE.Models;
using Microsoft.EntityFrameworkCore;

namespace AmitalBE.Context
{
    public class MainDBContext : DbContext
    {
        public MainDBContext ( DbContextOptions options ) : base(options) { }

        public DbSet<Models.Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
