using Application.Domain.Common.Entity;

namespace Domain.EntityModels
{
    public class User : BaseEntity, IEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password_Salts { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
        public bool AuthenticatedEmail { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}