using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int RequesterId { get; set; }
        [JsonIgnore]
        public Requester? Requester { get; set; }  
        
        public int AttendantId { get; set; }
        [JsonIgnore]
        public Attendant? Attendant { get; set; }


        public StatusTicketEnum Status { get; set; } = StatusTicketEnum.pending;
        public PriorityEnum Priority { get; set; } = PriorityEnum.low;
        public CategoryEnum Category { get; set; }
        public int Evaluation { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 

    }
}
