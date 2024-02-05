namespace Presentation.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.Log(LogLevel.Information, "========================================================");
            _logger.Log(LogLevel.Information, "REQUEST PATH: " + context.Request.Path);

            await _next(context);

            var contentType = context.Response.ContentType;

            _logger.Log(LogLevel.Information, "========================================================");

            _logger.Log(LogLevel.Information, contentType);
        }
    }
}
