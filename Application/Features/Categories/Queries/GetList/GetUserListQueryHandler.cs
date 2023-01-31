using Application.Services.BidService;
using Application.Services.CategoryService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<Category>>
    {

        private readonly ICategoryService _categoryService;


        public GetCategoryListQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<List<Category>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAll();
        }
    }
}