namespace Pim.Helpdesk.Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
