using System.ComponentModel.DataAnnotations;

namespace TodoProjectUsingCleanArchitecture.Contract.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email or Username is required")]
        public string Email_Or_Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
