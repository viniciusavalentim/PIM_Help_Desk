namespace Pim.Helpdesk.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> Login(string email, string password);
    }
}
