using Microsoft.EntityFrameworkCore;
using Pim.Helpdesk.Domain.Entities;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
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

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversations");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Title).IsRequired().HasMaxLength(250);
                entity.Property(c => c.UserId).IsRequired();

                entity.HasIndex(c => c.UserId);

                entity.HasMany(c => c.Messages)             
                      .WithOne(m => m.Conversation)         
                      .HasForeignKey(m => m.ConversationId) 
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("ChatMessages");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Role).IsRequired().HasMaxLength(10);
                entity.Property(m => m.Content).IsRequired();

                entity.HasIndex(m => m.ConversationId);
            });

        }
    }
}
