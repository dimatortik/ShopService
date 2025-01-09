using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Models;

namespace ShopService.Infrastructure.Data.Configuration;

public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
{
    public void Configure(EntityTypeBuilder<PurchaseItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.Property(x => x.Amount)
            .HasColumnType("numeric")
            .IsRequired();
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}