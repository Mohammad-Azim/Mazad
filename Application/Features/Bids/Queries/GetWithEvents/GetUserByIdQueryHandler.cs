
using Application.Services.BidService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public class GetBidByIdQueryHandler : IRequestHandler<GetBidByIdQuery, Bid>
    {
        private readonly IBidService _bidService;

        public GetBidByIdQueryHandler(IBidService bidService)
        {
            _bidService = bidService;
        }
        public async Task<Bid> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bidService.GetById(request.Id);
        }
    }
}