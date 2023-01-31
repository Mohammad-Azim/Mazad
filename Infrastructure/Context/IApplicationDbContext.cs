
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();

    }
}