using System.Data;
using Application.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly ApplicationDbContext _context;
        public CreateUserCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(8).WithMessage("Password Should Be At Least 8 Character");
            RuleFor(u => u.Password).NotNull().NotEmpty().Equal(u => u.ConfirmPassword).WithMessage("Password And Confirm Password Should Be The Same");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email Should be Valid Email");
            RuleFor(u => u.FirstName).NotEmpty().NotNull().WithMessage("First Name Should Enter");
            RuleFor(u => u.LastName).NotEmpty().NotNull().WithMessage("Last Name Should Enter");
            RuleFor(u => u.Email)
                .MustAsync(EmailNotExist)
                .WithMessage("Email Is Already Used");
        }

        private async Task<bool> EmailNotExist(string emailAddress, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking().Where(x => x.Email == emailAddress).FirstOrDefaultAsync(cancellationToken);
            return user != null;
        }
    }
}