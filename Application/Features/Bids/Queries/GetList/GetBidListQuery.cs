using MediatR;

namespace Application.Features.Bids.Queries.GetList
{
    public record GetBidListQuery : IRequest<GetListBidQueryResponse> { }
}