using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Services
{
    public interface ITicketService
    {
        Task<ServiceResponse<List<Ticket>>> FindTickets();
        Task<ServiceResponse<List<Ticket>>> FindTicketsByUser(Guid id);
        Task<ServiceResponse<Ticket>> FindTicketById(int id);
        Task<ServiceResponse<Ticket>> StoreTicket(StoreTicketDto request);
        Task<ServiceResponse<Ticket>> UpdateTicket(StoreTicketDto request);
        Task<ServiceResponse<Ticket>> DeleteTicket(StoreTicketDto request); 
    }
}
