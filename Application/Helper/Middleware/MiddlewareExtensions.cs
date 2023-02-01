using Microsoft.AspNetCore.Builder;

namespace Application.Helper.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlerMiddleware>();
            builder.UseMiddleware<BidMiddleware>();
            return builder;
        }
    }
}