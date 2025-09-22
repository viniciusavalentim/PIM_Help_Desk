using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Infrastructure.Context.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


    }
}
