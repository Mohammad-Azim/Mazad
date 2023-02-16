using Microsoft.AspNetCore.Builder;

namespace Application.Helper.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder builder)
        {
            //builder.UseMiddleware<CorsMiddleware>();
            builder.UseMiddleware<ExceptionHandlerMiddleware>();
            builder.UseMiddleware<ResponseCodeStatusHandlerMiddleware>();
            return builder;
        }
    }
}