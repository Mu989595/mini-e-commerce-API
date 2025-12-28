using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIDotNet.DTO;
using WebAPIDotNet.Services;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> userManager;
        private readonly JWTService jwtService;

        public AcountController(UserManager<ApplicationUser> UserManager, JWTService JWTService)
        {
            this.userManager = UserManager;
            this.jwtService = JWTService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO UserfromRequest)
        {
            if (ModelState.IsValid)
            {
                // save db 
                ApplicationUser user = new ApplicationUser();
                user.Name = UserfromRequest.Name;
                user.Email = UserfromRequest.Email;
                user.UserName = UserfromRequest.Email; // Typically UserName is set to Email

                IdentityResult result =
                    await userManager.CreateAsync(user, UserfromRequest.Password);

                if (result.Succeeded)
                {
                    return Ok("Created");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO UserfromRequest)
        {
            if (ModelState.IsValid)
            {
                // Trim email to remove any whitespace
                string email = UserfromRequest.Email.Trim();
                
                // Try to find user by email (Identity normalizes emails, so this should work)
                ApplicationUser? user = await userManager.FindByEmailAsync(email);
                
                // If not found by email, try by username (since we set UserName = Email during registration)
                if (user == null)
                {
                    user = await userManager.FindByNameAsync(email);
                }
                
                if (user != null)
                {
                    // Check password
                    bool isPasswordValid = await userManager.CheckPasswordAsync(user, UserfromRequest.Password);
                    
                    if (isPasswordValid)
                    {
                        // Get user roles
                        var roles = await userManager.GetRolesAsync(user);
                        
                        // Generate JWT Token
                        var token = jwtService.GenerateToken(user, roles);
                        
                        return Ok(new 
                        { 
                            Message = "Login successful", 
                            Token = token,
                            UserId = user.Id, 
                            UserName = user.UserName,
                            Email = user.Email,
                            Name = user.Name,
                            Roles = roles
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid password. Please check your password and try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "User not found. Please check your email address or register first.");
                }
            }

            return BadRequest(ModelState);
        }
    }
}
