
using Application.Domain.Entity;

namespace Domain.EntityModels
{
    public class User : BaseEntity, IEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}