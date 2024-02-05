namespace Application.Models
{
    public enum RequestType
    {
        Register,
        Login,
        RefreshToken
    }
    public class RequestModel
    {
        public RequestType Type { get; set; }
        public string Payload { get; set; } = null!;
    }
}
