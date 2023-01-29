using Domain.EntityModels;
using Infrastructure.Context;

namespace Application.Services.BidService
{
    public class BidService : GenericService<Bid>, IBidService
    {
        public BidService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}