using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Models;

namespace ShopService.Infrastructure.Data.Configuration;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CreatedAt);
        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        builder.Property(x => x.TotalAmount)
            .HasColumnType("numeric")
            .IsRequired();

        builder.HasMany(x => x.Items);
    }
}