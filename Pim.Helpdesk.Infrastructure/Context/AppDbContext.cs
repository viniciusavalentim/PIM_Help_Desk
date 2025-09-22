using Microsoft.EntityFrameworkCore;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<TicketResponse> TicketResponses { get; set; } = null!;
        public DbSet<Requester> Requesters { get; set; } = null!;
        public DbSet<Attendant> Attendants { get; set; } = null!;
        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Requester 1:1 (UserId é PK)
            modelBuilder.Entity<Requester>()
                .HasKey(r => r.UserId);

            // Attendant 1:1
            modelBuilder.Entity<Attendant>()
                .HasKey(a => a.UserId);

            // Administrator 1:1
            modelBuilder.Entity<Administrator>()
                .HasKey(a => a.UserId);

            modelBuilder.Entity<Administrator>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Administrator>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket -> Requester (obrigatório), restrito na deleção
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Requester)
                .WithMany()
                .HasForeignKey(t => t.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> Attendant (opcional)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Attendant)
                .WithMany()
                .HasForeignKey(t => t.AttendantId)
                .OnDelete(DeleteBehavior.Restrict);

            // TicketResponse -> Ticket
            modelBuilder.Entity<TicketResponse>()
                .HasOne(tr => tr.Ticket)
                .WithMany(t => t.TicketResponses)
                .HasForeignKey(tr => tr.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // TicketResponse -> User
            modelBuilder.Entity<TicketResponse>()
                .HasOne(tr => tr.User)
                .WithMany()
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
