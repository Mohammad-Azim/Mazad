using System.Net;
using Application.Helper.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Helper.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await ConvertException(httpContext, ex);
            }
        }

        private static async Task ConvertException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new BaseResponse<string>
            {
                Success = false,
                StatusCode = CodeStatusEnum.InternalServerError,
                Message = ex.Message
            };
            string output = JsonConvert.SerializeObject(response);
            await context.Response.WriteAsync(output);
        }
    }
}