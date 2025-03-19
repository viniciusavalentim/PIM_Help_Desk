using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Requester
    {
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

    }
}
