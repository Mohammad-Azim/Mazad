namespace API.Helper.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder builder)
        {

            builder.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/bid-hub"), appBuilder =>
            {
                // builder.UseMiddleware<CorsMiddleware>();
                //builder.UseMiddleware<ExceptionHandlerMiddleware>();
                appBuilder.UseMiddleware<ResponseCodeStatusHandlerMiddleware>();
            });
            return builder;
        }
    }
}