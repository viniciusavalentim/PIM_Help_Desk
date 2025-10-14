using MediatR;

namespace Pim.Helpdesk.Domain.Command.Ticket.FinishTicket
{
    public class FinishTicketCommand : IRequest<FinishTicketCommandResponse>
    {
        public required Guid TicketId { get; set; }
        public required Guid UserId { get; set; }
    }
}
