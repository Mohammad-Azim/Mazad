namespace Application.Features.Bids.Dtos
{
    public record BidDto
    {
        public int BidPrice { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}