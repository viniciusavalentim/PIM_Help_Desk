using Microsoft.EntityFrameworkCore;
using Pim.Helpdesk.Domain.Dto;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using PIM_Help_Desk.Enums;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Infrastructure.Context.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTicket(Guid requesterId, string title, string description, int statusId, int priorityId, int categoryId)
        {
            try
            {
                var findRequester = await _context.Users.FindAsync(requesterId);

                if (findRequester == null || findRequester.UserType != UserTypeEnum.Requester) return false;

                var ticket = new Ticket
                {
                    Title = title,
                    Description = description,
                    Category = Enum.Parse<CategoryEnum>(categoryId.ToString()),
                    CreatedAt = DateTime.Now,
                    RequesterId = findRequester.Id,
                    Priority = Enum.Parse<PriorityEnum>(priorityId.ToString()),
                    Status = StatusTicketEnum.pending,
                };

                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateTicketResponse(Guid ticketId, Guid userId, string title)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(ticketId);
                if (ticket == null) return false;

                if (ticket.AttendantId != userId)
                {
                    return false;
                }

                var ticketResponse = new TicketResponse
                {
                    CreatedAt = DateTime.Now,
                    Description = title,
                    TicketId = ticketId,
                    UserId = userId
                };

                await _context.TicketResponses.AddAsync(ticketResponse);
                var changes = await _context.SaveChangesAsync();

                return changes > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> FinishTicket(Guid ticketId, Guid userId)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(ticketId);
                if (ticket == null) return false;

                if (ticket.AttendantId != userId)
                {
                    return false;
                }

                ticket.Status = StatusTicketEnum.resolved;
                ticket.UpdatedAt = DateTime.Now;
                var changes = await _context.SaveChangesAsync();

                return changes > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AcceptTicket(Guid ticketId, Guid userId)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(ticketId);
                if (ticket == null) return false;
                var attendant = await _context.Users.FindAsync(userId);
                if (attendant == null || attendant.UserType != UserTypeEnum.Attendant) return false;
                if (ticket.Status != StatusTicketEnum.pending)
                {
                    return false;
                }
                ticket.AttendantId = userId;
                ticket.Status = StatusTicketEnum.in_progress;
                ticket.UpdatedAt = DateTime.Now;
                var changes = await _context.SaveChangesAsync();
                return changes > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TicketDto?> GetTicketById(Guid ticketId)
        {
            try
            {
                var ticketDto = await _context.Tickets
                    .Include(t => t.TicketResponses)
                    .Where(t => t.Id == ticketId)
                    .Select(ticket => new TicketDto
                    {
                        Id = ticket.Id,
                        Title = ticket.Title,
                        Description = ticket.Description,
                        Status = ticket.Status,
                        Priority = ticket.Priority,
                        Category = ticket.Category,
                        CreatedAt = ticket.CreatedAt,
                        RequesterId = ticket.RequesterId,
                        AttendantId = ticket.AttendantId,
                        TicketResponses = ticket.TicketResponses
                            .Select(response => new TicketResponseDto
                            {
                                Id = response.Id,
                                Description = response.Description,
                                CreatedAt = response.CreatedAt,
                                UserId = response.UserId
                            }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return ticketDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o ticket com ID {ticketId}: {ex.Message}", ex);
            }
        }

        public async Task<List<TicketDto>> GetTickets(
            Guid? requesterId,
            Guid? attendantId,
            string? searchText,
            int? priority,
            int? status,
            DateTime? startDate,
            DateTime? endDate)
        {
            try
            {
                IQueryable<Ticket> query = _context.Tickets
                                           .Include(t => t.TicketResponses);

                if (requesterId.HasValue)
                {
                    query = query.Where(t => t.RequesterId == requesterId);
                }

                if (attendantId.HasValue)
                {
                    query = query.Where(t => t.AttendantId == attendantId);
                }

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string search = searchText.ToLower();
                    query = query.Where(t =>
                        (t.Title != null && t.Title.ToLower().Contains(search)) ||
                        (t.Description != null && t.Description.ToLower().Contains(search)));
                }

                if (status.HasValue)
                {
                    query = query.Where(t => t.Status == (StatusTicketEnum)status.Value);
                }

                if (priority.HasValue)
                {
                    query = query.Where(t => t.Priority == (PriorityEnum)priority.Value);
                }

                if (startDate.HasValue)
                {
                    query = query.Where(t => t.CreatedAt >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    DateTime inclusiveEndDate = endDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    query = query.Where(t => t.CreatedAt <= inclusiveEndDate);
                }

                return await query
                 .Select(ticket => new TicketDto
                 {
                     Id = ticket.Id,
                     Title = ticket.Title,
                     Description = ticket.Description,
                     Status = ticket.Status,
                     Priority = ticket.Priority,
                     Category = ticket.Category,
                     CreatedAt = ticket.CreatedAt,
                     RequesterId = ticket.RequesterId,
                     AttendantId = ticket.AttendantId,
                     TicketResponses = ticket.TicketResponses
                         .Select(response => new TicketResponseDto
                         {
                             Id = response.Id,
                             Description = response.Description,
                             CreatedAt = response.CreatedAt,
                             UserId = response.UserId
                         }).ToList()
                 })
                 .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateStatusTicket(Guid ticketId, int status, Guid userId)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(ticketId);
                if (ticket == null) return false;

                if (ticket.AttendantId != userId)
                {
                    return false;
                }

                ticket.Status = (StatusTicketEnum)status;
                ticket.UpdatedAt = DateTime.Now;
                var changes = await _context.SaveChangesAsync();

                return changes > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
