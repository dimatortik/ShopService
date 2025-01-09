using Microsoft.EntityFrameworkCore;
using ShopService.Domain.Models;

namespace ShopService.Infrastructure.Data.DbInitializer;

public class DbInitializer(ShopDbContext context) : IDbInitializer
{
    public void Initialize()
    {
        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            return;
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                Customer.Create("John", "Doe", "A", new DateOnly(1985, 5, 15)).Value!,
                Customer.Create("Jane", "Smith", "B", new DateOnly(1990, 7, 20)).Value!,
                Customer.Create("Michael", "Johnson", "C", new DateOnly(1978, 3, 10)).Value!,
                Customer.Create("Emily", "Davis", "D", new DateOnly(1995, 12, 25)).Value!,
                Customer.Create("David", "Brown", "E", new DateOnly(1982, 8, 30)).Value!
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
        
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                Product.Create("Product1", 10.99m, "SKU001", "Category1").Value!,
                Product.Create("Product2", 15.49m, "SKU002", "Category2").Value!,
                Product.Create("Product3", 7.99m, "SKU003", "Category3").Value!,
                Product.Create("Product4", 20.00m, "SKU004", "Category4").Value!,
                Product.Create("Product5", 5.75m, "SKU005", "Category5").Value!
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }
        
        if (!context.Purchases.Any())
        {
            var random = new Random();
            var purchases = new List<Purchase>();
            var customers = context.Customers.ToList();
            var products = context.Products.ToList();
            
            foreach (var customer in customers)
            {
                var purchase = customer.AddPurchase().Value!;
                purchases.Add(purchase);
                foreach (var product in products)
                {
                    purchase.AddItem(product.Id, random.Next(1, 10), product.Price);
                }
            }
            context.Purchases.AddRange(purchases);
            context.SaveChanges();
        }
    }
}