
using Application.Domain.Entity;

namespace Domain.EntityModels
{
    public class Bid : BaseEntity, IEntity
    {
        public int BidPrice { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
    }
}