using Microsoft.EntityFrameworkCore;
using ShopService.Domain.Models;

namespace ShopService.Infrastructure.Data;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseItem> PurchaseItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
    }
}