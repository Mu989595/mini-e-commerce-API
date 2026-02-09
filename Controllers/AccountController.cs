using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;
using Mini_E_Commerce_API.Services;

namespace Mini_E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager, JWTService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            // 1. Convert DTO to ApplicationUser
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            // 2. Attempt to create the user and hash the password
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return Ok(new GeneralResponse { IsSuccess = true, Message = " Succeeded Register " });
            }

            // 3. In case of failure, send errors (e.g., weak password)
                return BadRequest(new GeneralResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault()?.Description ?? "Registration failed."
                });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            // 1. Search for the user by username
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null || string.IsNullOrEmpty(loginDto.Password))
            {
                return Unauthorized(new GeneralResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Username or Password"
                });
            }

            // 2. Verify password correctness
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (isPasswordCorrect)
                {
                    var token = await _jwtService.GenerateToken(user);
                    return Ok(new GeneralResponse
                    {
                        IsSuccess = true,
                        Message = "Login Successful",
                        Data = new { user.UserName, user.Email, Token = token }
                    });
                }
                else // Password is not correct
                {
                    // 3. In case of username or password failure, return a unified message for security
                    return Unauthorized(new GeneralResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid Username or Password"
                    });
                }
        }
    }
}
