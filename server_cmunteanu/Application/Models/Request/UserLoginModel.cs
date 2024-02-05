using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
