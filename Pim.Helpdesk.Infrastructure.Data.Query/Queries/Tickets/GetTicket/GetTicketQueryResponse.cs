using Pim.Helpdesk.Domain.Dto;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets
{
    public class GetTicketQueryResponse
    {
        public TicketDto? Ticket { get; set; }

    }
}
