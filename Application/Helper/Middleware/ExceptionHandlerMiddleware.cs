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
                return; // #??# is return necessary here ? debug it
            }
        }

        private static async Task ConvertException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new BaseResponse<string>
            {
                Success = false,
                StatusCode = CodeStatusEnum.InternalServerError,
                Message = ex.Message
            };
            string output = JsonConvert.SerializeObject(response);
            await httpContext.Response.WriteAsync(output);
        }
    }
}