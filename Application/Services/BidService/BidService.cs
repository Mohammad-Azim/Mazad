using Domain.EntityModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.BidService
{
    public class BidService : GenericService<Bid>, IBidService
    {
        private readonly DbSet<Bid> _bidEntities;
        private readonly DbSet<Product> _productEntities;

        public BidService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _bidEntities = dbContext.Set<Bid>();
            _productEntities = dbContext.Set<Product>();
        }

        public async Task<int> GetLargestBidPrice(int productId)
        {
            var largest = await _bidEntities.AsNoTracking().Where(x => x.ProductId == productId).MaxAsync(x => x.BidPrice);
            if (largest != 0)
            {
                return largest;
            }
            return (await _productEntities.AsNoTracking().Where(x => x.Id == productId).FirstOrDefaultAsync()).StartingPrice;
        }

        public async Task<List<Bid>> GetBidListByProduct(int id)
        {
            return await _bidEntities.AsNoTracking().Where(b => b.ProductId == id).ToListAsync();
        }
    }
}