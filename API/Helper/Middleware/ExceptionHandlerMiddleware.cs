using System.Net;
using Application.Helper.Response;
using Newtonsoft.Json;

namespace API.Helper.Middleware
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
                return;
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