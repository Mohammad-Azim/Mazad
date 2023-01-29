
namespace Application.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int PeriodOfUse { get; set; }
        public string State { get; set; } = null!;
        public int StartingPrice { get; set; }
        public DateTime end_time { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public int BidId { get; set; }
        public int CategoryId { get; set; }
    }
}