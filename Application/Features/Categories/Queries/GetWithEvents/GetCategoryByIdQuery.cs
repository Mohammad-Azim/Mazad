using Application.Services.CategoryService;
using MediatR;

namespace Application.Features.Categories.Queries.GetWithEvents
{
    public record GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var getCategoryByIdQueryResponse = new GetCategoryByIdQueryResponse();

            var data = await _categoryService.GetById(request.Id);
            if (data != null)
            {
                getCategoryByIdQueryResponse.SuccessResponse(data);
            }
            else
            {
                getCategoryByIdQueryResponse.NotFoundResponse();
            }
            return getCategoryByIdQueryResponse;
        }
    }
}