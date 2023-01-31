using Application.Services.BidService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Bids.Queries.GetList
{
    public class GetBidListQueryHandler : IRequestHandler<GetBidListQuery, List<Bid>>
    {
        private readonly IBidService _bidService;


        public GetBidListQueryHandler(IBidService bidService)
        {
            _bidService = bidService;
        }
        public async Task<List<Bid>> Handle(GetBidListQuery request, CancellationToken cancellationToken)
        {
            return await _bidService.GetAll();
        }
    }
}