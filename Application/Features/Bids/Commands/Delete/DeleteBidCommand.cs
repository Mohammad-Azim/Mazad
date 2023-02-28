using MediatR;

namespace Application.Features.Bids.Commands.Delete
{
    public record DeleteBidCommand : IRequest<DeleteBidCommandResponse>
    {
        public int Id { get; set; }
    }
}