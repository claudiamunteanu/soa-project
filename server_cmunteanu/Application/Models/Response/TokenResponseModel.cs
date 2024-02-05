namespace Application.Models.Response
{
    public class TokenResponseModel
    {
        public string AccessToken { get; set; } = null!;
        public string AccessTokenExpiration { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
