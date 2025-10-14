using MediatR;

namespace Pim.Helpdesk.Domain.Command.CreateTicketResponse
{
    public class CreateTicketResponseCommand : IRequest<CreateTicketResponseCommandResponse>
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
    }
}
