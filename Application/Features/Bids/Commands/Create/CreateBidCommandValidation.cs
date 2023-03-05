using FluentValidation;
using Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bids.Commands.Create
{
    public class CreateBidCommandValidation : AbstractValidator<CreateBidCommand>
    {
        private readonly ApplicationDbContext _context;
        public CreateBidCommandValidation(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(b => b.BidPrice).NotEqual(0).GreaterThan(0).WithMessage("Please Bid Price should be more than 0");
            RuleFor(b => b.ProductId).NotNull().WithMessage("You Should specify Product Id");
            RuleFor(b => b)
                .MustAsync(IsBidNotFromOwner)
                .WithMessage("Can't bid on your own product");

            RuleFor(b => b)
                .MustAsync(IsLastBidIsLargest)
                .WithMessage("Bid Should Be larger Than Last Bid");

            RuleFor(b => b)
                .MustAsync(IsLargerThanStartingPrice)
                .WithMessage("Bid Should Be more than the Starting Price");
        }

        private async Task<bool> IsBidNotFromOwner(CreateBidCommand b, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == b.ProductId, cancellationToken);
            if (product == null)
            {
                return false;
            }
            return product.OwnerId != b.UserId;
        }

        private async Task<bool> IsLastBidIsLargest(CreateBidCommand b, CancellationToken cancellationToken)
        {
            var largestBidPrice = await _context.Bids
                .Where(x => x.ProductId == b.ProductId)
                .OrderByDescending(x => x.BidPrice)
                .Select(x => x.BidPrice)
                .FirstOrDefaultAsync(cancellationToken);

            return b.BidPrice > largestBidPrice;
        }
        private async Task<bool> IsLargerThanStartingPrice(CreateBidCommand b, CancellationToken cancellationToken)
        {
            var startingPrice = (await _context.Products.SingleOrDefaultAsync(x => x.Id == b.ProductId, cancellationToken)).StartingPrice;
            return b.BidPrice > startingPrice;
        }
    }
}