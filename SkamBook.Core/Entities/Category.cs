namespace SkamBook.Core.Entities;

public class Category : BaseEntity
{
    public Category(string description)
    {
        Description = description;
        CreatedAt = DateTime.Now;
    }

    public Category() { }

    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public List<UserCategory> UserCategories { get; private set; }
}
