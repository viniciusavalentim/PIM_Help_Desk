using MediatR;

namespace Pim.Helpdesk.Domain.Command.ChatRequest
{
    public class ChatRequestCommand : IRequest<ChatRequestCommandResponse>
    {
        public Guid? ConversationId { get; set; }
        public Guid UserId { get; set; }
        public required string Message { get; set; }
        public List<ChatMessageDto> History { get; set; } = new();
    }

    public record ChatMessageDto(string Role, string Content);
}