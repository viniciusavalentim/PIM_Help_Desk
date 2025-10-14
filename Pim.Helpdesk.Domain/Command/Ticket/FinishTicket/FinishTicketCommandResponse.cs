namespace Pim.Helpdesk.Domain.Command.Ticket.FinishTicket
{
    public class FinishTicketCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
