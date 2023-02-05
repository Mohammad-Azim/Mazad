using Application.Features.Categories.Dtos;
using FluentValidation;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateBidCommandValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateBidCommandValidation()
        {
            RuleFor(s => s.Name).NotEmpty().NotNull().WithMessage("Please Add Category Name");
        }
    }
}