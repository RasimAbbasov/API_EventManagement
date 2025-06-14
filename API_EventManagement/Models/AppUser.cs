using Microsoft.AspNetCore.Identity;

namespace API_EventManagement.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
