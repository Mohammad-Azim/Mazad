using Application.Services.ProductService;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService ProductService)
        {
            _productService = ProductService;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.Delete(request.Id);
        }
    }
}