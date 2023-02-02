
using Application.Domain.Common.Entity;
namespace Domain.EntityModels
{
    public class Category : BaseEntity, IEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}