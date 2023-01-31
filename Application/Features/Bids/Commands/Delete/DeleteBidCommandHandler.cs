using Application.Services.BidService;
using AutoMapper;
using MediatR;

namespace Application.Features.Bids.Commands.Delete
{
    public class DeleteBidCommandHandler : IRequestHandler<DeleteBidCommand, int>
    {
        private readonly IBidService _bidService;

        public DeleteBidCommandHandler(IBidService ProductService)
        {
            _bidService = ProductService;
        }

        public async Task<int> Handle(DeleteBidCommand request, CancellationToken cancellationToken)
        {
            return await _bidService.Delete(request.Id);
        }
    }
}