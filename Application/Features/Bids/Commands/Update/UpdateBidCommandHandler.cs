
using Application.Services.BidService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Bids.Commands.Update
{
    public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand, Bid>
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;

        public UpdateBidCommandHandler(IBidService bidService, IMapper mapper)
        {
            _bidService = bidService;
            _mapper = mapper;
        }

        public async Task<Bid> Handle(UpdateBidCommand command, CancellationToken cancellationToken)
        {
            Bid Bid = _mapper.Map<Bid>(command);
            var value = await _bidService.GetById(Bid.Id);
            if (value != null)
                return await _bidService.Update(Bid);
            return null;
        }
    }
}

