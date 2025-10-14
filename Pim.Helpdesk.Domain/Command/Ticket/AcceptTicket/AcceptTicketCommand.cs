using MediatR;

namespace Pim.Helpdesk.Domain.Command.Ticket.AcceptTicket
{
    public class AcceptTicketCommand : IRequest<AcceptTicketCommandResponse>
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
    }
}
