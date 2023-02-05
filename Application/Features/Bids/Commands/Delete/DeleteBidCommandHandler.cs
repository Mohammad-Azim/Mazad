using Application.Services.BidService;
using MediatR;

namespace Application.Features.Bids.Commands.Delete
{
    public class DeleteBidCommandHandler : IRequestHandler<DeleteBidCommand, DeleteBidCommandResponse>
    {
        private readonly IBidService _bidService;

        public DeleteBidCommandHandler(IBidService ProductService)
        {
            _bidService = ProductService;
        }

        public async Task<DeleteBidCommandResponse> Handle(DeleteBidCommand request, CancellationToken cancellationToken)
        {
            var deleteBidCommandResponse = new DeleteBidCommandResponse();

            var result = await _bidService.Delete(request.Id);

            if (result == 0)
            {
                deleteBidCommandResponse.NotFoundResponse();
            }
            if (deleteBidCommandResponse.Success)
            {
                deleteBidCommandResponse.SuccessResponse();
            }
            return deleteBidCommandResponse;
        }
    }
}