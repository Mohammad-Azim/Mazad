
namespace Domain.EntityModels
{
    public class Bid
    {
        public int Id { get; set; }
        public int BidPrice { get; set; }
        public DateTime Date { get; set; }
        public int Products_Id { get; set; }
        public int UserId { get; set; }
    }
}