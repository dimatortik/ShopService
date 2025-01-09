using ShopService.Domain.ValueObjects;
using ShopService.Shared;

namespace ShopService.Domain.Models;

public sealed class Customer
{
    public static readonly DateOnly MinBirthDate = new DateOnly(1900, 1, 1);
    public static readonly DateOnly MaxBirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18));
    
    private readonly List<Purchase> _purchases = [];

    private Customer()
    { }
    
    private Customer(DateOnly birthDate)
    {
        Id = Guid.NewGuid();
        BirthDate = birthDate;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<Purchase> Purchases => _purchases;
    
    public static Result<Customer> Create(string firstName, string lastName, string middleName,DateOnly birthDate)
    {
        var fullNameResult = FullName.Create(firstName, lastName, middleName);
        var customer = new Customer(birthDate)
        {
            FullName = fullNameResult
        };
        return Result.Success(customer);
    }
    
    public Result<Purchase> AddPurchase()
    {
        var result = Purchase.Create(Id);
        if (result.IsFailure)
        {
            return Result.Failure<Purchase>(result.Error);
        }
        _purchases.Add(result.Value!);
        return Result.Success(result.Value!);
    }
    
}