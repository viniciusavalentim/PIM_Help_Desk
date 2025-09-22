using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Administrator
    {
        [Key]
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
