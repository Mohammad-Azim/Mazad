using Application.Services.CategoryService;
using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var deleteCategoryCommandResponse = new DeleteCategoryCommandResponse();

            var result = await _categoryService.Delete(request.Id);
            if (result == 0)
            {
                deleteCategoryCommandResponse.NotFoundResponse();
            }
            if (deleteCategoryCommandResponse.Success)
            {
                deleteCategoryCommandResponse.SuccessResponse();
            }
            return deleteCategoryCommandResponse;
        }
    }
}