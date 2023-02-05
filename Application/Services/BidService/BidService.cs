using Domain.EntityModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.BidService
{
    public class BidService : GenericService<Bid>, IBidService
    {
        private readonly DbSet<Bid> _bidEntities;

        public BidService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _bidEntities = dbContext.Set<Bid>();
        }

        public async Task<int> GetLargestBidPrice()
        {
            return await _bidEntities.AsNoTracking().MaxAsync(x => x.BidPrice);
        }
    }
}