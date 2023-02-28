using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
    {
        public int Id { get; set; }
    }
}