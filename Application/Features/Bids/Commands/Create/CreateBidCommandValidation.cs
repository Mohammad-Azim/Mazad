using FluentValidation;

namespace Application.Features.Bids.Commands.Create
{
    public class CreateBidCommandValidation : AbstractValidator<CreateBidCommand>
    {
        public CreateBidCommandValidation()
        {
            RuleFor(s => s.BidPrice).NotEqual(0).GreaterThan(0).WithMessage("Please Bid Price should be more than 0");
        }
    }
}