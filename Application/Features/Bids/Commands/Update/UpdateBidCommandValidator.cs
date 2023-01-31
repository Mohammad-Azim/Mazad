using Application.Features.Bids.Dtos;
using FluentValidation;

namespace Application.Features.Bids.Commands.Update
{
    public class UpdateBidCommandValidator : AbstractValidator<BidDto>
    {
        public UpdateBidCommandValidator()
        {
            RuleFor(s => s.BidPrice).NotEqual(0).GreaterThan(0).WithMessage("Please Bid Price should be more than 0");
        }
    }
}