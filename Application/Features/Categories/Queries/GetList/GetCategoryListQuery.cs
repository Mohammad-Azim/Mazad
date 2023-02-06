using MediatR;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetCategoryListQuery : IRequest<GetListCategoryQueryResponse> { }
}