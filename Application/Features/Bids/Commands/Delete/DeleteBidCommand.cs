using MediatR;

namespace Application.Features.Bids.Commands.Delete
{
    public class DeleteBidCommand : IRequest<DeleteBidCommandResponse>
    {
        public int Id { get; set; }
    }
}