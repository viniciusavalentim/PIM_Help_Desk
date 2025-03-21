using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> FindUsers();
        Task<ServiceResponse<User>> FindUserById(Guid id);
        Task<ServiceResponse<UserRequesterDto>> StoreRequester(StoreUserDto request);
        Task<ServiceResponse<UserRequesterDto>> UpdateRequester(StoreUserDto request);
        Task<ServiceResponse<UserRequesterDto>> DeleteRequester(StoreUserDto request);
        Task<ServiceResponse<UserAttendantDto>> StoreAttendant(StoreUserDto request);
        Task<ServiceResponse<UserAttendantDto>> UpdateAttendant(StoreUserDto request);
        Task<ServiceResponse<UserAttendantDto>> DeleteAttendant(StoreUserDto request);

    }
}
