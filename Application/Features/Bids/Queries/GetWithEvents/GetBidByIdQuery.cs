using MediatR;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public record GetBidByIdQuery : IRequest<GetBidByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}