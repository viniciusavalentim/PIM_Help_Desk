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

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
