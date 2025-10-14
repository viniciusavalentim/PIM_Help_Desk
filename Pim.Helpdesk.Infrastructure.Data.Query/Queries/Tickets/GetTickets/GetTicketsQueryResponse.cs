using Pim.Helpdesk.Domain.Dto;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets
{
    public class GetTicketsQueryResponse
    {
        public List<TicketDto>? Tickets { get; set; }
    }
}
