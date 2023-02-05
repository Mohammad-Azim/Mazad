using Application.Services.ProductService;
using MediatR;

namespace Application.Features.Products.Queries.GetWithEvents
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly IProductService _productService;


        public GetProductByIdQueryHandler(IProductService ProductService)
        {
            _productService = ProductService;
        }
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var getProductByIdQueryResponse = new GetProductByIdQueryResponse();

            var data = await _productService.GetById(request.Id);
            if (data != null)
            {
                getProductByIdQueryResponse.SuccessResponse(data);
            }
            else
            {
                getProductByIdQueryResponse.NotFoundResponse();
            }
            return getProductByIdQueryResponse;
        }


    }
}