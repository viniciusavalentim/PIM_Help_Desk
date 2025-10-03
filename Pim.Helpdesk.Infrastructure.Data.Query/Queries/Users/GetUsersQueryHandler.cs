using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            GetUsersQueryResponse users = new GetUsersQueryResponse();
            try
            {
                users.Users = await _userRepository.GetAllUsers();
                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
