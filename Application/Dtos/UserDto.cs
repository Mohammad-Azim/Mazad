
namespace Application.Dtos
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}