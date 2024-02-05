using Microsoft.AspNetCore.Identity;

namespace IdentityDomain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
