using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets
{
    public class GetTicketQuery : IRequest<GetTicketQueryResponse>
    {
        [Required]
        public required Guid TicketId { get; set; }
    }
}
