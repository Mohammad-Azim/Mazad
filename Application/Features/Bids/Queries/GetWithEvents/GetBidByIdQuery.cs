using Application.Services.BidService;
using MediatR;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public record GetBidByIdQuery : IRequest<GetBidByIdQueryResponse>
    {
        public int Id { get; set; }
    }
    public class GetBidByIdQueryHandler : IRequestHandler<GetBidByIdQuery, GetBidByIdQueryResponse>
    {
        private readonly IBidService _bidService;

        public GetBidByIdQueryHandler(IBidService bidService)
        {
            _bidService = bidService;
        }
        public async Task<GetBidByIdQueryResponse> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
        {
            var getBidByIdQueryResponse = new GetBidByIdQueryResponse();
            var data = await _bidService.GetById(request.Id);
            if (data != null)
            {
                getBidByIdQueryResponse.SuccessResponse(data);
            }
            else
            {
                getBidByIdQueryResponse.NotFoundResponse();
            }
            return getBidByIdQueryResponse;
        }
    }
}