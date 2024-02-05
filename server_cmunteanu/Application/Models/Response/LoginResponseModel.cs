namespace Application.Models.Response
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; } = null!;
        public string AccessTokenExpiration { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        //public string RefreshTokenExpiration { get; set; } = null!;
    }
}
