using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            GetUserQueryResponse user = new GetUserQueryResponse();
            try
            {
                user.User = await _userRepository.GetUser(request.UserId);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
