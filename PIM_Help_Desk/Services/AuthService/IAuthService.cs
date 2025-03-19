using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<User>> Register(UserDto request);
        Task<ServiceResponse<string>> Login(UserDto request);
    }
}