using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }
}