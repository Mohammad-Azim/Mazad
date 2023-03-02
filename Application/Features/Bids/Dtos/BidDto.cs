using Application.Helper.Profiles;
using Domain.EntityModels;

namespace Application.Features.Bids.Dtos
{
    public record BidDto : IMapFrom<Bid>
    {
        public int BidPrice { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}