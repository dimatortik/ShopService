using ShopService.Shared;

namespace ShopService.Domain.ValueObjects;

public class FullName : ValueObject
{
    public const int MaxLength = 50;
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; } 
    
    public static FullName Create(string firstName, string lastName)
    {
        return new FullName(firstName, lastName);
    }
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}