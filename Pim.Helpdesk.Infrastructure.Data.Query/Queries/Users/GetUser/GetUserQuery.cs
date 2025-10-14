using MediatR;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users.GetUser
{
    public class GetUserQuery : IRequest<GetUserQueryResponse>
    {
        public required Guid UserId { get; set; }
    }
}
