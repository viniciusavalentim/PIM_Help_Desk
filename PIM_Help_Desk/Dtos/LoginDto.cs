using System.ComponentModel.DataAnnotations;

namespace PIM_Help_Desk.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
