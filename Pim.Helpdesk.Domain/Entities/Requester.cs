using System.ComponentModel.DataAnnotations;

namespace PIM_Help_Desk.Models
{
    public class Requester
    {
        [Key]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}
