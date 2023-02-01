using Application.Services.ProductService;
using FluentValidation;

namespace Application.Features.Bids.Commands.Create
{
    public class CreateBidCommandValidation : AbstractValidator<CreateBidCommand>
    {

        private readonly IProductService _productService;
        public CreateBidCommandValidation(IProductService productService)
        {
            _productService = productService;

            RuleFor(s => s.BidPrice).NotEqual(0).GreaterThan(0).WithMessage("Please Bid Price should be more than 0");
            RuleFor(s => s.ProductId).NotNull().WithMessage("You Should specify Product Id");



        }

        // private async Task<bool> IsNotProductOwner(CreateBidCommand command, CancellationToken cancellationToken)
        // {
        //     var product = await _productService.GetById(command.ProductId);
        //     if (product.OwnerId == command.UserId)
        //     {
        //         return false;
        //     }
        //     return true;
        // }
    }
}