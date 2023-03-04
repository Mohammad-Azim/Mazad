using Domain.EntityModels;
using Application.Context;

namespace Application.Services.ProductService
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}