using Microsoft.EntityFrameworkCore;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Context.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> UserExists(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = password,
                    CreatedAt = DateTime.UtcNow,
                    UserType = PIM_Help_Desk.Enums.UserTypeEnum.Requester
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
