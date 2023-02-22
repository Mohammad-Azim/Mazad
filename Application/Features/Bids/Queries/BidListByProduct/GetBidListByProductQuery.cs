using MediatR;

namespace Application.Features.Bids.Queries.BidListByProduct
{
    public class GetBidListByProductQuery : IRequest<GetListBidByProductQueryResponse>
    {
        public int Id { get; set; }

    }
}