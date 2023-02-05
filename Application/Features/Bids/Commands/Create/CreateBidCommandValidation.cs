using Application.Services.BidService;
using Application.Services.ProductService;
using FluentValidation;

namespace Application.Features.Bids.Commands.Create
{
    public class CreateBidCommandValidation : AbstractValidator<CreateBidCommand>
    {
        private readonly IProductService _productService;
        private readonly IBidService _bidService;
        public CreateBidCommandValidation(IProductService productService, IBidService bidService)
        {
            _productService = productService;
            _bidService = bidService;

            RuleFor(b => b.BidPrice).NotEqual(0).GreaterThan(0).WithMessage("Please Bid Price should be more than 0");
            RuleFor(b => b.ProductId).NotNull().WithMessage("You Should specify Product Id");
            RuleFor(b => b)
                .MustAsync(IsBidNotFromOwner)
                .WithMessage("Can't bid on your own product");

            RuleFor(b => b.BidPrice)
                .MustAsync(IsLastBidIsLargest)
                .WithMessage("Bid Should Be larger Than Last Bid");
        }

        private async Task<bool> IsBidNotFromOwner(CreateBidCommand b, CancellationToken token)
        {
            var product = await _productService.GetById(b.ProductId);
            if (product == null)
            {
                return false;
            }
            return product.OwnerId != b.UserId;
        }

        private async Task<bool> IsLastBidIsLargest(int bidPrice, CancellationToken token)
        {
            var largestBidPrice = await _bidService.GetLargestBidPrice();
            return bidPrice > largestBidPrice;
        }
    }
}