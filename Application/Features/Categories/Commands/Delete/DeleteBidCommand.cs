using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
    {
        public int Id { get; set; }
    }
}