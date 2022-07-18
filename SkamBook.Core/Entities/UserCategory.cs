namespace SkamBook.Core.Entities;

public class UserCategory //: BaseEntity
{
    public UserCategory(Guid userId, Guid categoryId)
    {
        UserId = userId;
        CategoryId = categoryId;
        CreatedAt = DateTime.Now;
    }

    public UserCategory() { }

    public DateTime CreatedAt { get; private set; }
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    
}
