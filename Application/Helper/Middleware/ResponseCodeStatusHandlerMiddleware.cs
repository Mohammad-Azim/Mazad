using Application.Helper.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Helper.Middleware
{
    public class ResponseCodeStatusHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseCodeStatusHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            Stream originalBody = httpContext.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    httpContext.Response.Body = memStream;

                    await _next(httpContext);

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();
                    ConvertResponseCode(httpContext, responseBody);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                httpContext.Response.Body = originalBody;
            }
        }

        private static void ConvertResponseCode(HttpContext httpContext, string responseBody)
        {
            BaseResponse<object> responseObject = JsonConvert.DeserializeObject<BaseResponse<object>>(responseBody);

            httpContext.Response.StatusCode = (int)responseObject.StatusCode;
        }
    }
}