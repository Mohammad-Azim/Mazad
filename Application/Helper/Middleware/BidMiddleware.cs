using Application.Features.Bids.Commands.Create;
using Application.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Application.Helper.Middleware
{
    public class BidMiddleware
    {
        private readonly RequestDelegate _next;

        public BidMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IProductService productService)
        {
            var x = httpContext.Request.Path;
            if (x.Value == "/api/Bid")
            {
                var request = httpContext.Request;
                if ((request.Method == HttpMethods.Post || request.Method == HttpMethods.Put) && request.ContentLength > 0)
                {
                    request.EnableBuffering();
                    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                    await request.Body.ReadAsync(buffer);
                    //get body string here...
                    var requestContent = Encoding.UTF8.GetString(buffer);
                    var result = JsonConvert.DeserializeObject<CreateBidCommand>(requestContent);
                    if (await IsBidFromOwner(result, productService))
                    {
                        await ReturnErrorResponse(httpContext);
                        return;
                    }
                    request.Body.Position = 0;  //rewinding the stream to 0 #??#
                }
            }
            await _next(httpContext); // calling next middleware
        }

        private static async Task<bool> IsBidFromOwner(CreateBidCommand b, IProductService _productService)
        {
            var product = await _productService.GetById(b.ProductId);
            if (product != null)
                return product.OwnerId == b.UserId;
            return false;
        }

        private static async Task ReturnErrorResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var bytes = Encoding.UTF8.GetBytes("{\"status\":400,\"errors\":{\"OwnerId\":[\"you can't bid on your own product\"]}}");
            await context.Response.Body.WriteAsync(bytes);

            await context.Response.StartAsync();
        }
    }
}