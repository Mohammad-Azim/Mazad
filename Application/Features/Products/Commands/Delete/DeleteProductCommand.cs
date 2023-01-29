using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

    }
}