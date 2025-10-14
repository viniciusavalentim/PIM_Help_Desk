namespace Pim.Helpdesk.Domain.Command.Ticket.CreateTicket
{
    public class CreateTicketCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
