using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_Help_Desk.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public StatusTicketEnum Status { get; set; } = StatusTicketEnum.pending;
        public PriorityEnum Priority { get; set; } = PriorityEnum.low;
        public CategoryEnum Category { get; set; }
        public int Evaluation { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public List<TicketResponse>? TicketResponses { get; set; }

        [ForeignKey("Requester")]
        public Guid RequesterId { get; set; }
        public Requester Requester { get; set; } = null!;

        [ForeignKey("Attendant")]
        public Guid? AttendantId { get; set; }
        public Attendant? Attendant { get; set; }

    }
}
