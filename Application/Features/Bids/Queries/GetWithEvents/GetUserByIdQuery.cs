using MediatR;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public class GetBidByIdQuery : IRequest<GetBidByIdQueryResponse>
    {
        public int Id { get; set; }

    }
}