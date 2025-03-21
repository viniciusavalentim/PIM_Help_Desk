using PIM_Help_Desk.Enums;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Dtos
{
    public class StoreUserDto
    {
        public StatusUserEnum StatusUser { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Ramal { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

    }
}
