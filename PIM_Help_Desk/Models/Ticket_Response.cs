using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Ticket_Response
    {
        [Key]
        public int Id { get; set; }

        public int TicketId { get; set; }
        [JsonIgnore]
        public Ticket? Ticket { get; set; }

        public int RequesterId { get; set; }
        [JsonIgnore]
        public Requester? Requester { get; set; } 

        public int AttendantId { get; set; }
        [JsonIgnore]
        public Attendant? Attendant { get; set; }

        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
