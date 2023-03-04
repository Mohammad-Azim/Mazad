using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Context
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Owner)
                   .WithMany(u => u.Products);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products);
        }
    }
}