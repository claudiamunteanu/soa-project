using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models.Request
{
    public class UserCreateModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "The password should contain at least 8 characters!")]
        [RegularExpression(@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "The password must contain at least one uppecase letter, one lowercase letter and a digit!")]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;
    }
}
