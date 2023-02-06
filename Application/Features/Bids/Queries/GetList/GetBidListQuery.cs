using MediatR;

namespace Application.Features.Bids.Queries.GetList
{
    public class GetBidListQuery : IRequest<GetListBidQueryResponse> { }
}