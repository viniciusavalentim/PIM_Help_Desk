using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Infrastructure.Context.Repositories;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users;
using PIM_Help_Desk.Services.AuthService;

namespace Pim.Helpdesk
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
