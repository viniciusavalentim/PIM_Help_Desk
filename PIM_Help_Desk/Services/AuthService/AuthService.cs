using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PIM_Help_Desk.Data;
using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PIM_Help_Desk.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<ServiceResponse<User>> Register(UserDto request)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            try
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                };

                var hashedPassword = new PasswordHasher<User>()
               .HashPassword(newUser, request.Password);

                newUser.PasswordHash = hashedPassword;

                await _context.users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                serviceResponse.Message = "Usuário Criado com sucesso";
                serviceResponse.Data = newUser;
                return serviceResponse;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<string>> Login(UserDto request)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            try
            {
                var user = await _context.users.FirstOrDefaultAsync(u => u.Name == request.Name);

                if (user == null)
                {
                    serviceResponse.Message = "Usuário não encontrado";
                    serviceResponse.Status = false;
                    return serviceResponse;
                }

                if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                {
                    serviceResponse.Message = "Dados incorretos.";
                    serviceResponse.Status = false;
                    return serviceResponse;
                }

                string token = CreateToken(user);

                serviceResponse.Message = "Usuário logado com sucesso";
                serviceResponse.Status = true;
                serviceResponse.Data = token;
                return serviceResponse;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Status = false;
            }

            return serviceResponse;
        }


        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }
    }
}
