using Pim.Helpdesk.Domain.Command.ChatRequest;
using Pim.Helpdesk.Domain.Entities;

namespace Pim.Helpdesk.Domain.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<Conversation> StoreConversationHistoryAsync(Guid? conversationId, Guid userId, List<ChatMessageDto> fullHistory);
        Task<IEnumerable<Conversation>> GetConversationsByUserIdAsync(Guid userId);
    }
}
