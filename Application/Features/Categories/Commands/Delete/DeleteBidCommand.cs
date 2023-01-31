using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }

    }
}