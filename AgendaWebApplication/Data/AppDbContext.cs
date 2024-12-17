using AgendaWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<AgendaModel> Agenda { get; set; }

    }
}
