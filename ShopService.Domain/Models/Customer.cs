using ShopService.Domain.ValueObjects;
using ShopService.Shared;

namespace ShopService.Domain.Models;

public sealed class Customer
{
    public static readonly DateOnly MinBirthDate = new DateOnly(1900, 1, 1);
    public static readonly DateOnly MaxBirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18));
    
    private readonly List<Purchase> _purchases = [];
    
    private Customer(string firstName, string lastName, DateOnly birthDate)
    {
        Id = Guid.NewGuid();
        FullName = FullName.Create(firstName, lastName);
        BirthDate = birthDate;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<Purchase> Purchases => _purchases;
    
    public static Result<Customer> Create(string firstName, string lastName, DateOnly birthDate)
    {
        return Result.Success(new Customer(firstName, lastName, birthDate));
    }
    
    public Result AddPurchase(Guid customerId)
    {
        var result = Purchase.Create(customerId);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }
        _purchases.Add(result.Value!);
        return Result.Success();
    }
    
}