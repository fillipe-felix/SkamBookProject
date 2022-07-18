namespace SkamBook.Core.Entities;

public class User : BaseEntity
{
    protected User()
    {
        
    }

    public User(string fullName, DateTime birthDate, string email, long lat, long lon, List<Guid> categories)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Email = new Email(email);
        Active = true;
        Address = new Address(lat, lon);
        UserCategories = categories.Select(uc => new UserCategory(Id, uc)).ToList();
    }

    public string FullName { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public Email Email { get; private set; } = null!;
    public bool Active { get; private set; }
    public Address Address { get; private set; } = null!;

    public List<UserCategory> UserCategories { get; private set; }
    
}
