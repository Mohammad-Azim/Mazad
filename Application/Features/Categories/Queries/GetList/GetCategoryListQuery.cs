using MediatR;

namespace Application.Features.Categories.Queries.GetList
{
    public record GetCategoryListQuery : IRequest<GetListCategoryQueryResponse> { }
}