namespace Pim.Helpdesk.Domain.Command.Register
{
    public class RegisterCommandResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
