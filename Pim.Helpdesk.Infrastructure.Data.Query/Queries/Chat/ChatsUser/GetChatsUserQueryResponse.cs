namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Chat.ChatsUser
{
    public class GetChatsUserQueryResponse
    {
        public List<ConversationDto> Conversations { get; set; } = new();
    }
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ChatMessageDto> Messages { get; set; } = new();
    }

    public record ChatMessageDto(string Role, string Content);
}
