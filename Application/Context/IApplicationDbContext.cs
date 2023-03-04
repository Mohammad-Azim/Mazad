
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        Task<int> SaveChangesAsync();
    }
}