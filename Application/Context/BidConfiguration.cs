using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Context
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasOne(p => p.User)
            .WithMany(u => u.Bids);

            builder.HasOne(p => p.Product)
            .WithMany(u => u.Bids);
        }
    }
}