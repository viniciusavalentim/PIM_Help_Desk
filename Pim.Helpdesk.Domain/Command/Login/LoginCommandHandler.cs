using CleanArch.CrossCutting.Security.Security;
using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Services;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginCommandResponse();
            try
            {
                var loginResult = await _authService.Login(request.Email, request.Password);
                if (loginResult)
                {
                    response.Success = true;
                    response.Token = JwtTokenGenerator.GenerateToken(request.Email); ;
                    response.Message = "Login successful.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid email or password.";
                    return response;
                }

                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = $"An error occurred during login: {e.Message}";
                return response;
            }
        }


    }
}
