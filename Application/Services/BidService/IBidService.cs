using Domain.EntityModels;

namespace Application.Services.BidService
{
    public interface IBidService : IGenericService<Bid>
    {
        Task<int> GetLargestBidPrice(int id);
        Task<List<Bid>> GetBidListByProduct(int id);
    }
}