using ShopService.Shared;

namespace ShopService.Domain.ValueObjects;

public class FullName : ValueObject
{
    public const int MaxLength = 50;
    private FullName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; } 
    public string MiddleName { get; private set; }
    
    public static FullName Create(string firstName, string lastName, string middleName)
    {
        return new FullName(firstName, lastName, middleName);
    }
    
    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName}";
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}