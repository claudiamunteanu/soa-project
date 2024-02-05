using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ExceptionHandler
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString());

            if (context.Exception is ValidationException validationException)
            {
                var json = validationException.Errors;

                var statusCode = HttpStatusCode.BadRequest;

                var code = validationException.Errors.FirstOrDefault()?.ErrorCode;
                if (!string.IsNullOrEmpty(code) && int.TryParse(code, out var parsedCode))
                {
                    statusCode = (HttpStatusCode)parsedCode;
                }

                context.Result = statusCode switch
                {
                    HttpStatusCode.Conflict => new ConflictObjectResult(json),
                    _ => new BadRequestObjectResult(json)
                };

                context.HttpContext.Response.StatusCode = (int)statusCode;
            }
        }
    }
}
