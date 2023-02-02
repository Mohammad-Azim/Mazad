using Application.Services.ProductService;
using AutoMapper;
using Domain.Common.Response;
using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService ProductService)
        {
            _productService = ProductService;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var deleteProductCommandResponse = new DeleteProductCommandResponse();

            var result = await _productService.Delete(request.Id);
            if (result == 0)
            {
                deleteProductCommandResponse.Success = false;
                deleteProductCommandResponse.Message = "Deleting Failed: Product Not Found";
                deleteProductCommandResponse.StatusCode = CodeStatusEnum.NotFound;
            }
            if (deleteProductCommandResponse.Success)
            {
                deleteProductCommandResponse.Message = "Product Deleted Successfully";
                deleteProductCommandResponse.StatusCode = CodeStatusEnum.Ok;
            }
            return deleteProductCommandResponse;
        }
    }
}