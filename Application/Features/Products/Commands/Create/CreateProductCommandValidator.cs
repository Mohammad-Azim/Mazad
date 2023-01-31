

using FluentValidation;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("Please Add Name To the Product");

            RuleFor(s => s.Image).NotNull().NotEmpty().WithMessage("Please Add Image To the Product");

            RuleFor(s => s.State).NotNull().NotEmpty().WithMessage("Please Add State To the Product");

            RuleFor(s => s.StartingPrice).NotNull().NotEmpty().GreaterThan(0).WithMessage("Please Add StartingPrice To the Product");

            RuleFor(s => s.State).NotNull().NotEmpty().WithMessage("Please Add State To the Product");

            RuleFor(s => s.EndTime).NotNull().NotEmpty().Must(CheckEndTime).WithMessage("EndTime To the Product should be more than 20 hour from now");
        }

        private bool CheckEndTime(DateTime time)
        {
            if (DateTime.UtcNow.AddHours(20) <= time)
            {
                return true;
            }
            return false;
        }
    }
}