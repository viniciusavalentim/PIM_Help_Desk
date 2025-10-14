using Pim.Helpdesk.Domain.Dto;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<bool> CreateTicket(Guid userId, string title, string description, int statusId, int priorityId, int categoryId);
        Task<List<TicketDto>> GetTickets(Guid? requesterId, Guid? attendantId, string? searchText, int? priority, int? status, DateTime? startDate, DateTime? endDate);
        Task<TicketDto> GetTicketById(Guid ticketId);
        Task<bool> FinishTicket(Guid ticketId, Guid userId);
        Task<bool> UpdateStatusTicket(Guid ticketId, int status, Guid userId);
        Task<bool> CreateTicketResponse(Guid ticketId, Guid userId, string title);
        Task<bool> AcceptTicket(Guid ticketId, Guid userId);
    }
}
