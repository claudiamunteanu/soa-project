
using System.Net;

namespace IdentityProvider.Models
{
    public class ResponseModel
    {
        public string Payload { get; set; } = null!;

        public bool IsSuccessStatusCode { get; set; } = true;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
