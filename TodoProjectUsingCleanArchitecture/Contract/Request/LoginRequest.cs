using System.ComponentModel.DataAnnotations;

namespace TodoProjectUsingCleanArchitecture.Contract.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
