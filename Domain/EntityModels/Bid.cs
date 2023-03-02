
using Application.Domain.Common.Entity;

namespace Domain.EntityModels
{
    public class Bid : BaseEntity, IEntity
    {
        public int BidPrice { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}