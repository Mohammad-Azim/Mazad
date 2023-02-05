using MediatR;

namespace Application.Features.Products.Queries.GetList
{
    public class GetProductListQuery : IRequest<GetListProductQueryResponse> { }
}