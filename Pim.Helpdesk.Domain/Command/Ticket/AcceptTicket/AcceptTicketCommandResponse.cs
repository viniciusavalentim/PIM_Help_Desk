namespace Pim.Helpdesk.Domain.Command.Ticket.AcceptTicket
{
    public class AcceptTicketCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
