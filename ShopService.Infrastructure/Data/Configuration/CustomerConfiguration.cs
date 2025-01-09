using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Models;
using ShopService.Domain.ValueObjects;

namespace ShopService.Infrastructure.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.BirthDate);
        builder.OwnsOne(x => x.FullName, y =>
        {
            y.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(FullName.MaxLength)
                .IsRequired();
            y.Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(FullName.MaxLength)
                .IsRequired();
            y.Property(x => x.MiddleName)
                .HasColumnName("MiddleName")
                .HasMaxLength(FullName.MaxLength)
                .IsRequired();
        });
        builder.Property(x => x.BirthDate)
            .HasColumnType("date")
            .HasColumnName("BirthDate")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasMany(x => x.Purchases)
            .WithOne(p => p.Customer)
            .HasForeignKey(p => p.CustomerId);

    }
}