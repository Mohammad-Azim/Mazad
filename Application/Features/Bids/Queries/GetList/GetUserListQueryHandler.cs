using Application.Services.BidService;
using MediatR;

namespace Application.Features.Bids.Queries.GetList
{
    public class GetBidListQueryHandler : IRequestHandler<GetBidListQuery, GetListBidQueryResponse>
    {
        private readonly IBidService _bidService;

        public GetBidListQueryHandler(IBidService bidService)
        {
            _bidService = bidService;
        }
        public async Task<GetListBidQueryResponse> Handle(GetBidListQuery request, CancellationToken cancellationToken)
        {
            var getListBidQueryResponse = new GetListBidQueryResponse();
            var data = await _bidService.GetAll();
            getListBidQueryResponse.SuccessResponse(data);
            return getListBidQueryResponse;
        }
    }
}