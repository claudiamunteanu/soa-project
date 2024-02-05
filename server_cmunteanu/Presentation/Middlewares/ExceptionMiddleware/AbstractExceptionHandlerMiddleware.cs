using System.Net;

namespace Presentation.Middlewares.ExceptionMiddleware
{
    public abstract class AbstractExceptionHandlerMiddleware
    {
        private readonly ILogger<AbstractExceptionHandlerMiddleware> _logger;

        private readonly RequestDelegate _next;
        public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

        public AbstractExceptionHandlerMiddleware(RequestDelegate next, ILogger<AbstractExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                // log the error
                _logger.LogError(exception.Message, "Error during executing {Context}", context.Request.Path.Value);

                var response = context.Response;
                response.ContentType = "application/json";

                // get the response code and message
                var (status, message) = GetResponse(exception);
                response.StatusCode = (int)status;
                await response.WriteAsync(message);
            }
        }
    }
}
