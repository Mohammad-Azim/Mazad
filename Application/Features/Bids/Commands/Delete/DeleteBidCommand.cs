using Application.Context;
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
        private readonly IDeleteBidCommandResponse _response;

        public DeleteBidCommandHandler(ApplicationDbContext context, IDeleteBidCommandResponse response)
        {
            _context = context;
            _response = response;
        }

        public async Task<DeleteBidCommandResponse> Handle(DeleteBidCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Bids.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (result != null)
            {
                _context.Bids.Remove(result);
                await _context.SaveChangesAsync(cancellationToken);
                _response.SuccessResponse();
            }
            else
            {
                _response.NotFoundResponse();
            }
            return (DeleteBidCommandResponse)_response;
        }
    }
}