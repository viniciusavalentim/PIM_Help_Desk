using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Dtos
{
    public class AuthUserDto
    {
        public User? User { get; set; }
        public string token { get; set; } = string.Empty;   
    }
}
