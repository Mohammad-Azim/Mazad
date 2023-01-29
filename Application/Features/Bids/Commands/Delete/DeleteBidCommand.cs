using MediatR;

namespace Application.Features.Bids.Commands.Delete
{
    public class DeleteBidCommand : IRequest<int>
    {
        public int Id { get; set; }

    }
}