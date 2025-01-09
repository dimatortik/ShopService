using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Models;

namespace ShopService.Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Category);
        builder.Property(x => x.Title)
            .HasMaxLength(Product.TitleMaxLength)
            .IsRequired();
        builder.Property(x => x.Price)
            .HasColumnType("numeric")
            .IsRequired();
        builder.Property(x => x.Sku)
            .HasMaxLength(Product.SkuMaxLength)
            .IsRequired();
    }
}