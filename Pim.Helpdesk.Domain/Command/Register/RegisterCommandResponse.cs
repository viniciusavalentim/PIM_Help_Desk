using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Command.Register
{
    public class RegisterCommandResponse
    {
        public User? User { get; set; }
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
