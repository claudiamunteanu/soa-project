namespace IdentityProvider.Models.Response
{
    public class LoginModel
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }
    }
}
