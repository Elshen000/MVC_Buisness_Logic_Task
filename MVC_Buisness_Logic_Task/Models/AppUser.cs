using Microsoft.AspNetCore.Identity;

namespace MVC_Buisness_Logic_Task.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsDeactive { get; set; }
    }
}
