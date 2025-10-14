using MediatR;

namespace Pim.Helpdesk.Domain.Command.Ticket.UpdateStatusTicket
{
    public class UpdateStatusTicketCommand : IRequest<UpdateStatusTicketCommandResponse>
    {
        public required Guid TicketId { get; set; }
        public required Guid UserId { get; set; }
        public required int Status { get; set; }
    }
}
