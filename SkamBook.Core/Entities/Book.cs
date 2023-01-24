namespace SkamBook.Core.Entities;

public class Book : BaseEntity
{
    public Book()
    {
        
    }
    
    public Book(Guid userId, string name, string author, string description, List<Guid> bookCategories, List<Guid> bookImages)
    {
        UserId = userId;
        Name = name;
        Author = author;
        Description = description;
        BookCategories = bookCategories.Select(uc => new BookCategory(Id, uc)).ToList();
        BookImages = bookImages.Select(bi => new BookImage(Id, bi)).ToList();
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }
    
    public string Name { get; private set; }
    public string Author { get; private set; }
    public string Description { get; private set; }
    public List<BookCategory> BookCategories { get; private set; }
    
    public List<BookImage> BookImages { get; private set; }
}
