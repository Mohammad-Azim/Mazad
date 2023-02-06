using System.Data;
using Application.Services.UserService;
using FluentValidation;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;
        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(8).WithMessage("Password Should Be At Least 8 Character");
            RuleFor(u => u.Password).NotNull().NotEmpty().Equal(u => u.ConfirmPassword).WithMessage("Password And Confirm Password Should Be The Same");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email Should be Valid Email");
            RuleFor(u => u.FirstName).NotEmpty().NotNull().WithMessage("First Name Should Enter");
            RuleFor(u => u.LastName).NotEmpty().NotNull().WithMessage("Last Name Should Enter");
            RuleFor(u => u.Email)
                .MustAsync(EmailNotExist)
                .WithMessage("Email Is Already Used");
        }

        private async Task<bool> EmailNotExist(string emailAddress, CancellationToken token)
        {
            return !await _userService.EmailExist(emailAddress);
        }
    }
}