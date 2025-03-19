using PIM_Help_Desk.Enums;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Attendant
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public string Ramal { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public StatusUserEnum Status { get; set; }
    }
}