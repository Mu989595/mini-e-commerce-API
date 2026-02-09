using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_API.Models; // Ensure ApplicationUser is defined in this namespace or update accordingly

namespace Mini_E_Commerce_API.Models
{

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> categories { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}