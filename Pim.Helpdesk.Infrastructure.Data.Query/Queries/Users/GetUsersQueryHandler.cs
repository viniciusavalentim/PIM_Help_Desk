using MediatR;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetUsersQueryResponse());
        }
    }
}
