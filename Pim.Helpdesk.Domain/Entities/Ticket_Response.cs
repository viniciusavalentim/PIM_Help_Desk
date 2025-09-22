using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_Help_Desk.Models
{
    public class TicketResponse
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
