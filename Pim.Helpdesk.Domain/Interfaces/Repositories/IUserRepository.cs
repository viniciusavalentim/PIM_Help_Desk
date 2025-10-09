using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> UserExists(string email);
        Task<List<User>> GetAllUsers();
    }
}
