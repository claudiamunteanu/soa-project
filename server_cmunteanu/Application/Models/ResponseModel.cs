using System.Net;

namespace Application.Models
{
    public class ResponseModel
    {
        public string Payload { get; set; } = null!;

        public bool IsSuccessStatusCode { get; set; } = true;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
