using Application.Services.CategoryService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Queries.GetWithEvents
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetById(request.Id);
        }
    }
}