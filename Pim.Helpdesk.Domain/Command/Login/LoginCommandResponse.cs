using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Command.Login
{
    public class LoginCommandResponse
    {
        public bool Success { get; set; }
        public User? User { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
