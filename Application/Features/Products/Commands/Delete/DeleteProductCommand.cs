using Application.Services.ProductService;
using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }
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
                deleteProductCommandResponse.NotFoundResponse();
            }
            if (deleteProductCommandResponse.Success)
            {
                deleteProductCommandResponse.SuccessResponse();
            }
            return deleteProductCommandResponse;
        }
    }
}