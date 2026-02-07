using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAPIDotNet.Controllers;

namespace WebAPIDotNet.Models;

public class Context : IdentityDbContext<ApplicationUser>
{
    public DbSet<Employee> Employee { get; set; } = null!;
  //  public DbSet<Department> Department { get; set; } = null!;
    
    public Context(DbContextOptions<Context> options) : base(options)
    {

    }
}
