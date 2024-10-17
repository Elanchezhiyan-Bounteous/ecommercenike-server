using System.ComponentModel.DataAnnotations;

namespace ecommercenike_server.Contracts
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
