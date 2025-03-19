using Microsoft.EntityFrameworkCore;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Attendant> attendant { get; set; }
    }
}
