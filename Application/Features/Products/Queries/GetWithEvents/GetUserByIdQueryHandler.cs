
using Application.Services.ProductService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Queries.GetWithEvents
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductService _productService;


        public GetProductByIdQueryHandler(IProductService ProductService)
        {
            _productService = ProductService;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetById(request.Id);
        }


    }
}