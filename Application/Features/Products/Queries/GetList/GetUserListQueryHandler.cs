using Application.Features.Products.Queries.GetList;
using Application.Services.ProductService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Queries.GetList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<Product>>
    {
        private readonly IProductService _productService;


        public GetProductListQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAll();
        }
    }
}