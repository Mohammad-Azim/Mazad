using Application.Services.CategoryService;
using MediatR;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, GetListCategoryQueryResponse>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryListQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<GetListCategoryQueryResponse> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var getListCategoryQueryResponse = new GetListCategoryQueryResponse();
            var data = await _categoryService.GetAll();
            getListCategoryQueryResponse.SuccessResponse(data);
            return getListCategoryQueryResponse;
        }
    }
}