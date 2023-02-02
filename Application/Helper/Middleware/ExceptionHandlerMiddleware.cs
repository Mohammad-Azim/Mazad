using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Application.Helper.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            _logger.LogInformation("MyMiddleware executing.. MyCustomMiddleware");
        }
    }
}