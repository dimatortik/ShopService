using ShopService.Shared;

namespace ShopService.Domain.Models;

public sealed class Purchase
{
    private readonly IList<PurchaseItem> _items = [];
    private Purchase(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    
    public DateTime CreatedAt { get; private set; }

    public decimal TotalAmount { get; private set; }
    
    public Customer Customer { get; set; }

    public IReadOnlyCollection<PurchaseItem> Items => _items.AsReadOnly();

    public static Result<Purchase> Create(Guid customerId)
    {
        return Result.Success(new Purchase(customerId));
    }
    
    public Result AddItem(Guid productId, int quantity, decimal price)
    {
        var result = PurchaseItem.Create(productId, quantity, price);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }
        _items.Add(result.Value!);
        TotalAmount += result.Value!.Amount; 
        return Result.Success();
    }
    
    public Result RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null)
        {
            return Result.Failure(new Error("purchase.error", "Item not found"));
        }
        TotalAmount -= item.Amount;
        _items.Remove(item);
        return Result.Success();
    }
}