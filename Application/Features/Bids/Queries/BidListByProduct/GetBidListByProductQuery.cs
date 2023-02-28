using Application.Services.BidService;
using MediatR;

namespace Application.Features.Bids.Queries.BidListByProduct
{
    public record GetBidListByProductQuery : IRequest<GetListBidByProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetListBidByProductQueryHandler : IRequestHandler<GetBidListByProductQuery, GetListBidByProductQueryResponse>
    {
        private readonly IBidService _bidService;

        public GetListBidByProductQueryHandler(IBidService bidService)
        {
            _bidService = bidService;
        }
        public async Task<GetListBidByProductQueryResponse> Handle(GetBidListByProductQuery request, CancellationToken cancellationToken)
        {
            var getListBidQueryResponse = new GetListBidByProductQueryResponse();
            var data = await _bidService.GetBidListByProduct(request.Id);

            if (data != null)
            {
                getListBidQueryResponse.SuccessResponse(data);
            }
            else
            {
                getListBidQueryResponse.NotFoundResponse();
            }
            return getListBidQueryResponse;
        }
    }
}