using MediatR;

namespace Application.Features.Bids.Queries.BidListByProduct
{
    public record GetBidListByProductQuery : IRequest<GetListBidByProductQueryResponse>
    {
        public int Id { get; set; }

    }
}