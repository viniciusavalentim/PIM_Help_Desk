using PIM_Help_Desk.Enums;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Log
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public LogTypeEnum LogType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 

    }
}
