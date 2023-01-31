using Domain.EntityModels;

namespace Application.Features.Products.Dtos
{
    public record ProductDto
    {
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int PeriodOfUse { get; set; }
        public string State { get; set; } = null!;
        public int StartingPrice { get; set; }

        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }


    }
}