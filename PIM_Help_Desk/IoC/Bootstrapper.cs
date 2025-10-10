using Pim.Helpdesk.Domain.Command.Login;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Infrastructure.Context.Repositories;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users;
using Pim.Helpdesk.Domain.Interfaces.Services;
using Pim.Helpdesk.Domain.Services.AuthService;
using Pim.Helpdesk.Domain.Command.Register;

namespace Pim.Helpdesk
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
