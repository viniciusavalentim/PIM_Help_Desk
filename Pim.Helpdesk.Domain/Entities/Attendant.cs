using System.ComponentModel.DataAnnotations;

namespace PIM_Help_Desk.Models
{
    public class Attendant
    {
        [Key]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Ramal { get; set; } = string.Empty;
    }
}