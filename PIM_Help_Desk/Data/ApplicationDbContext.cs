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
        public DbSet<Requester> requester { get; set; }
        public DbSet<Administrator> administrator { get; set; }
        public DbSet<Log> log { get; set; }
        public DbSet<Ticket> ticket { get; set; }
        public DbSet<Ticket_Response> ticket_responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Requester)
                .WithMany()
                .HasForeignKey(t => t.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Attendant)
                .WithMany()
                .HasForeignKey(t => t.AttendantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
