using Pim.Helpdesk.Domain.Command.Login;
using Pim.Helpdesk.Domain.Command.Register;
using Pim.Helpdesk.Domain.Command.Ticket.CreateTicket;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Domain.Interfaces.Services;
using Pim.Helpdesk.Domain.Services.AuthService;
using Pim.Helpdesk.Infrastructure.Context.Repositories;
using Pim.Helpdesk.Infrastructure.Data.Context.Repositories;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users.GetUser;

namespace Pim.Helpdesk
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUserQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetTicketsQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetTicketsQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateTicketCommand).Assembly);
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
        }
    }
}
