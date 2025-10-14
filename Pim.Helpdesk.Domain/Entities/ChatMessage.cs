namespace Pim.Helpdesk.Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public int Sequence { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}
