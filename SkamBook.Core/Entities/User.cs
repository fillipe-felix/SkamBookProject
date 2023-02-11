namespace SkamBook.Core.Entities;

public class User : BaseEntity
{
    protected User()
    {
        
    }

    public User(string fullName, DateTime birthDate, string email, string lat, string lon, string city, List<Guid> categories, Image imageProfile)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Email = new Email(email);
        Active = true;
        Address = new Address(lat, lon, city);

        ImageProfile = imageProfile;
        
        UserCategories = categories.Select(uc => new UserCategory(Id, uc)).ToList();
    }

    public string FullName { get; private set; } = null!;

    public Image ImageProfile { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Email Email { get; private set; } = null!;
    public bool Active { get; private set; }
    public Address Address { get; private set; } = null!;

    public List<UserCategory> UserCategories { get; private set; }
    public List<Book> Books { get; private set; }
    public virtual ICollection<Conversation> Conversations { get; private set; }
    public virtual ICollection<MatchBook> LikedBooks { get; private set; }
}
