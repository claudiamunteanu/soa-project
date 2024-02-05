namespace IdentityProvider.Models.Response
{
    public class RefreshedTokenModel
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }
    }
}
