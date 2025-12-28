using Microsoft.AspNetCore.Identity;

namespace WebAPIDotNet.Controllers
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}