using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bids.Commands.Delete
{
    public record DeleteBidCommand : IRequest<DeleteBidCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteBidCommandHandler : IRequestHandler<DeleteBidCommand, DeleteBidCommandResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteBidCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteBidCommandResponse> Handle(DeleteBidCommand request, CancellationToken cancellationToken)
        {
            var deleteBidCommandResponse = new DeleteBidCommandResponse();

            var result = await _context.Bids.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (result != null)
            {
                _context.Bids.Remove(result);
                await _context.SaveChangesAsync(cancellationToken);
                deleteBidCommandResponse.SuccessResponse();
            }
            else
            {
                deleteBidCommandResponse.NotFoundResponse();
            }
            return deleteBidCommandResponse;
        }
    }
}