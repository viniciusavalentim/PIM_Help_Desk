using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, GetTicketQueryResponse>
    {

        private readonly ITicketRepository _ticketRepository;

        public GetTicketQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<GetTicketQueryResponse> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            GetTicketQueryResponse ticket = new GetTicketQueryResponse();
            try
            {
                ticket.Ticket = await _ticketRepository.GetTicketById(request.TicketId);

                return ticket;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
