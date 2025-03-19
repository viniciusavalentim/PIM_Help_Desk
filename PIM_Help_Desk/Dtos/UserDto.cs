using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;

namespace PIM_Help_Desk.Dtos
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
