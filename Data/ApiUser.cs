using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class ApiUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
