
namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}