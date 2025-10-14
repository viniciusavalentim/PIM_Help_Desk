using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, GetTicketsQueryResponse>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<GetTicketsQueryResponse> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            GetTicketsQueryResponse tickets = new GetTicketsQueryResponse();
            try
            {
                tickets.Tickets = await _ticketRepository.GetTickets(
                    request.RequesterId,
                    request.AttendantId,
                    request.SearchText,
                    request.Priority,
                    request.Status,
                    request.StartDate,
                    request.EndDate
                );

                return tickets;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
