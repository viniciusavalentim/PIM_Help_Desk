using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Dtos
{
    public class UserAttendantDto   
    {
        public User User { get; set; }
        public Attendant Attendant { get; set; }
    }
}
