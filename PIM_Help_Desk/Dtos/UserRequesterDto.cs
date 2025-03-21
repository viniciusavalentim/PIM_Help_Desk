using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Dtos
{
    public class UserRequesterDto
    {
        public User User { get; set; }
        public Requester Requester { get; set; }
    }
}
