using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }
}