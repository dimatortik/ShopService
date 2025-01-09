using ShopService.Shared;

namespace ShopService.Domain.Models;

public sealed class PurchaseItem
{
    
    private PurchaseItem(Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Amount { get; private set; } = 0;
    public Product Product { get; set; }

    public static Result<PurchaseItem> Create(Guid productId, int quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<PurchaseItem>(new Error( 
                "validation.error",
                "Quantity must be greater than zero"));
        }
        var item = new PurchaseItem(productId, quantity);
        item.Amount = item.Product.Price * quantity;
        
        return Result.Success(item);
    }
}