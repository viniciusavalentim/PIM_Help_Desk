using CleanArch.CrossCutting.Security.Security;
using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Domain.Interfaces.Services;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginCommandResponse();
            try
            {
                var loginResult = await _authService.Login(request.Email, request.Password);
                if (loginResult)
                {
                    var user = await _userRepository.GetUserByEmail(request.Email);
                    response.User = user;
                    response.Success = true;
                    response.Token = JwtTokenGenerator.GenerateToken(request.Email);
                    response.Message = "Login successful.";
                }
                else
                {
                    response.User = null;
                    response.Success = false;
                    response.Message = "Invalid email or password.";
                    return response;
                }

                return response;
            }
            catch (Exception e)
            {
                response.User = null;
                response.Success = false;
                response.Message = $"An error occurred during login: {e.Message}";
                return response;
            }
        }


    }
}
