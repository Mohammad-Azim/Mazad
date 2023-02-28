using Application.Services.ProductService;
using MediatR;

namespace Application.Features.Products.Queries.GetList
{
    public class GetProductListQuery : IRequest<GetListProductQueryResponse> { }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, GetListProductQueryResponse>
    {
        private readonly IProductService _productService;

        public GetProductListQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<GetListProductQueryResponse> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var getListProductQueryResponse = new GetListProductQueryResponse();
            var data = await _productService.GetAll();
            getListProductQueryResponse.SuccessResponse(data);
            return getListProductQueryResponse;
        }
    }
}