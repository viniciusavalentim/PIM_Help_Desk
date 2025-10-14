using Microsoft.EntityFrameworkCore;
using Pim.Helpdesk.Domain.Command.ChatRequest;
using Pim.Helpdesk.Domain.Entities;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using Pim.Helpdesk.Infrastructure.Context;

namespace Pim.Helpdesk.Infrastructure.Data.Context.Repositories
{
    public class ChatRepository : IChatRepository
    {

        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> StoreConversationHistoryAsync(Guid? conversationId, Guid userId, List<ChatMessageDto> fullHistory)
        {
            if (fullHistory == null || !fullHistory.Any())
            {
                throw new ArgumentException("A lista de histórico não pode ser vazia.", nameof(fullHistory));
            }

            Conversation? conversation = null;

            if (conversationId.HasValue && conversationId.Value != Guid.Empty)
            {
                conversation = await _context.Conversations
                    .Include(c => c.Messages)
                    .FirstOrDefaultAsync(c => c.Id == conversationId.Value && c.UserId == userId);
            }

            if (conversation != null)
            {
                _context.ChatMessages.RemoveRange(conversation.Messages);
                await _context.SaveChangesAsync();

                var newMessages = new List<ChatMessage>();
                int sequence = 0;
                foreach (var msgDto in fullHistory)
                {
                    newMessages.Add(new ChatMessage
                    {
                        Id = Guid.NewGuid(),
                        Role = msgDto.Role,
                        Content = msgDto.Content,
                        Sequence = sequence++,
                        CreatedAt = DateTime.UtcNow,
                        ConversationId = conversation.Id
                    });
                }
                await _context.ChatMessages.AddRangeAsync(newMessages);
                conversation.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                conversation = CreateNewConversation(userId, fullHistory.First().Content);

                int sequence = 0;
                foreach (var msgDto in fullHistory)
                {
                    conversation.Messages.Add(new ChatMessage
                    {
                        Id = Guid.NewGuid(),
                        Role = msgDto.Role,
                        Content = msgDto.Content,
                        Sequence = sequence++,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                await _context.Conversations.AddAsync(conversation);
            }

            await _context.SaveChangesAsync();

            return conversation;
        }
        private Conversation CreateNewConversation(Guid userId, string firstMessage)
        {
            return new Conversation
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = firstMessage.Length > 250 ? firstMessage.Substring(0, 250) : firstMessage,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByUserIdAsync(Guid userId)
        {
            return await _context.Conversations
                .Where(c => c.UserId == userId)
                .Include(c => c.Messages.OrderBy(m => m.Sequence)) 
                .OrderByDescending(c => c.UpdatedAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
