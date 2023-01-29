using Application.Services.BidService;
using Application.Services.ProductService;
using Application.Services.UserService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Bids.Commands.Create
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, Bid>
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IBidService _bidService;

        private readonly IMapper _mapper;

        public CreateBidCommandHandler(IProductService productService, IUserService userService, IBidService bidService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
            _userService = userService;
            _bidService = bidService;
        }

        public async Task<Bid> Handle(CreateBidCommand command, CancellationToken cancellationToken)
        {
            var user = await _userService.GetById(command.UserId);
            var product = await _productService.GetById(command.ProductId);
            if (user != null && product != null)
            {
                Bid bid = _mapper.Map<Bid>(command);
                bid.Date = DateTime.UtcNow;
                return await _bidService.Create(bid);
            }
            return null;
        }


    }
}