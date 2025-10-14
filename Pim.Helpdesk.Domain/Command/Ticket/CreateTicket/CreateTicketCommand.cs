using MediatR;

namespace Pim.Helpdesk.Domain.Command.Ticket.CreateTicket
{
    public class CreateTicketCommand : IRequest<CreateTicketCommandResponse>
    {
        public required Guid RequesterId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Category { get; set; }
        public required int Priority { get; set; }
    }
}
