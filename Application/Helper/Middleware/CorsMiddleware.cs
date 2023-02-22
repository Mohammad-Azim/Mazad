using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Application.Helper.Middleware
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;
        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            //httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            if (httpContext.Request.Headers["Origin"].FirstOrDefault() != "http://localhost:4222")
            {

                Console.WriteLine("I'm out");
                return;
            }
            await _next(httpContext);


        }
    }
}
