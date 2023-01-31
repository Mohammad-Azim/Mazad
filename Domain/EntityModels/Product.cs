

using Application.Domain.Entity;

namespace Domain.EntityModels
{
    public class Product : BaseEntity, IEntity
    {

        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int PeriodOfUse { get; set; }
        public string State { get; set; } = null!;
        public int StartingPrice { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public List<Bid> Bids { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
