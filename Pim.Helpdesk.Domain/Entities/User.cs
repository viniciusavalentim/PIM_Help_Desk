using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        //[Required]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]  
        public string Email { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;
                    
        public string Phone { get; set; } = string.Empty;

        //[Required]
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public StatusUserEnum Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public UserTypeEnum UserType { get; set; } = UserTypeEnum.Requester;
    }
}
