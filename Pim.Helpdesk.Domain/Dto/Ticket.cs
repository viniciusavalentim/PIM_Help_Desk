using PIM_Help_Desk.Enums;

namespace Pim.Helpdesk.Domain.Dto
{
    public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }

    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public StatusTicketEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }
        public CategoryEnum Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RequesterId { get; set; }
        public Guid? AttendantId { get; set; }
        public List<TicketResponseDto> TicketResponses { get; set; } = new();
    }
}
