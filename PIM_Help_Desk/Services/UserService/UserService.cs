using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Services.UserService
{
    public class UserService : IUserService
    {
        public Task<ServiceResponse<UserAttendantDto>> DeleteAttendant(StoreUserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<UserRequesterDto>> DeleteRequester(StoreUserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> FindUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<User>>> FindUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<UserAttendantDto>> StoreAttendant(StoreUserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<UserRequesterDto>> StoreRequester(StoreUserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<UserAttendantDto>> UpdateAttendant(StoreUserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<UserRequesterDto>> UpdateRequester(StoreUserDto request)
        {
            throw new NotImplementedException();
        }
    }
}
