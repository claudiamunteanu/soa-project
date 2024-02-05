using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models.Request
{
    public class RefreshTokenModel
    {
        [Required]
        public string? AccessToken { get; set; }
        [Required]
        public string? RefreshToken { get; set; }
    }
}
