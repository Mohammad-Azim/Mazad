using Application.Features.Users.Commands.Update;
using Application.Helper.Profiles;
using Domain.EntityModels;

namespace Application.Features.Users.Dtos
{
    public record UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}