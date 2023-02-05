using Application.Services.CategoryService;
using Application.Services.UserService;
using FluentValidation;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public CreateProductCommandValidator(IUserService userService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;

            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("Please Add Name To the Product");

            RuleFor(s => s.Image).NotNull().NotEmpty().WithMessage("Please Add Image To the Product");

            RuleFor(s => s.State).NotNull().NotEmpty().WithMessage("Please Add State To the Product");

            RuleFor(s => s.StartingPrice).GreaterThan(0).WithMessage("Please Add Starting Price To the Product and it should be more than 0");

            RuleFor(s => s.State).NotNull().NotEmpty().WithMessage("Please Add State To the Product");

            RuleFor(s => s.EndTime).NotNull().NotEmpty().Must(CheckEndTime).WithMessage("EndTime To the Product should be more than 20 hour from now");

            RuleFor(s => s.OwnerId).MustAsync(IsUserExist).WithMessage("Owner Does not exist");

            RuleFor(s => s.CategoryId).MustAsync(IsCategoryExist).WithMessage("Category Does not exist");
        }

        private bool CheckEndTime(DateTime time)
        {
            return DateTime.UtcNow.AddHours(20) <= time;
        }
        private async Task<bool> IsUserExist(int id, CancellationToken token)
        {
            var user = await _userService.GetById(id);
            return user != null;
        }

        private async Task<bool> IsCategoryExist(int id, CancellationToken token)
        {
            var category = await _categoryService.GetById(id);
            return category != null;
        }
        // #??# can i make it one method ( IsUserExist +   IsCategoryExist)
    }
}