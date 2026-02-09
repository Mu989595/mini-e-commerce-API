using System.ComponentModel.DataAnnotations;

namespace Mini_E_Commerce_API.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } // Ensure it's public and a regular set

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}