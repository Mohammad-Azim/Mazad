using Application.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateProductCommandValidator(ApplicationDbContext context)
        {
            _context = context;

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
        private async Task<bool> IsUserExist(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
            return user != null;
        }

        private async Task<bool> IsCategoryExist(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
            return category != null;
        }
    }
}