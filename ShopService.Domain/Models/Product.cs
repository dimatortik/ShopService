using ShopService.Shared;

namespace ShopService.Domain.Models;

public sealed class Product
{
    public const int TitleMaxLength = 100;
    public const int SkuMaxLength = 50;

    private Product()
    { }
    private Product(string name, decimal price, string sku, string category)
    {
        Id = Guid.NewGuid();
        Title = name;
        Price = price;
        Sku = sku;
        Category = category;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }
    public string Category { get; private set; }
    public string Sku { get; set; }
    public decimal Price { get; private set; }
    
    public static Result<Product> Create(string name, decimal price, string sku, string category)
    {
        return Result.Success(new Product(name, price, sku, category));
    }
}