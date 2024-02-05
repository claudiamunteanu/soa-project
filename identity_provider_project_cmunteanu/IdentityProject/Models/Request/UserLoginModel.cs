using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models.Request
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
