using CleanArch.CrossCutting.Security.Security;
using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Domain.Shared;

namespace Pim.Helpdesk.Domain.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            RegisterCommandResponse response = new RegisterCommandResponse();

            try
            {
                var existingUser = await _userRepository.UserExists(command.Email);
                if (existingUser != null)
                {
                    response.Success = false;
                    response.Message = "Usuário já cadastrado";
                    response.Token = null;
                    return response;
                }

                var hashedPassword = PasswordHasher.Hash(command.Password);
                var registerUser = await _userRepository.RegisterUser(command.Name, command.Email, hashedPassword);

                if (registerUser)
                {
                    response.Token = JwtTokenGenerator.GenerateToken(command.Email);
                    response.Success = true;
                    response.Message = "Usuário cadastrado com sucesso";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuário não cadastrado";
                    response.Token = null;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Token = null;
                return response;
            }
        }
    }
}
