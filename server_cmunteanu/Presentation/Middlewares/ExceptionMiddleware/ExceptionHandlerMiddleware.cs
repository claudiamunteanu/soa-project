using System.Net;
using Application.Exceptions;

namespace Presentation.Middlewares.ExceptionMiddleware
{
    public class ExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
    {
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<AbstractExceptionHandlerMiddleware> logger) : base(next, logger)
        {
        }

        public override (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            HttpStatusCode code;
            switch (exception)
            {
                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UserNotFound:
                    code = HttpStatusCode.NotFound;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            return (code, exception.Message);
        }
    }
}
