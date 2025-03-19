using System.ComponentModel.DataAnnotations;

namespace PIM_Help_Desk.Dtos
{
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
