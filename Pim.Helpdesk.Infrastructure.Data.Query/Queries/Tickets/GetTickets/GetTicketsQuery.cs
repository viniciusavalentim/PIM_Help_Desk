using MediatR;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets
{
    public class GetTicketsQuery : IRequest<GetTicketsQueryResponse>
    {
        public Guid? RequesterId { get; set; }
        public Guid? AttendantId { get; set; }
        public string? SearchText { get; set; }
        public int? Priority { get; set; }
        public int? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
