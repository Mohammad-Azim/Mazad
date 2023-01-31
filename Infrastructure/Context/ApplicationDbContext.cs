using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Owner)
            .WithMany(u => u.Products);

            modelBuilder.Entity<Bid>()
            .HasOne(p => p.user)
            .WithMany(u => u.Bids);

            modelBuilder.Entity<Bid>()
            .HasOne(p => p.product)
            .WithMany(u => u.Bids);


            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

    }
}