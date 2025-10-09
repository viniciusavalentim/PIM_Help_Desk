using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Domain.Interfaces.Services;
using Pim.Helpdesk.Domain.Shared;

namespace Pim.Helpdesk.Domain.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _userRepository.UserExists(email);
            if (user == null) return false;

            return PasswordHasher.Verify(password, user.PasswordHash) ? true : false;
        }
    }
}
