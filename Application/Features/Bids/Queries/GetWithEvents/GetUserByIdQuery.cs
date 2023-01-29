
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public class GetBidByIdQuery : IRequest<Bid>
    {
        public int Id { get; set; }

    }
}