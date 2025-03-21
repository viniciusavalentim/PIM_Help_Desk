using PIM_Help_Desk.Enums;

namespace PIM_Help_Desk.Dtos
{
    public class StoreTicketDto
    {
        public int RequesterId { get; set; }
        public string Title { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public StatusTicketEnum Status { get; set; } = StatusTicketEnum.pending;
        public PriorityEnum Priority { get; set; }
        public string Type { get; set; } = string.Empty;
    }   
}
