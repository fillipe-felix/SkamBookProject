namespace SkamBook.Core.Entities;

public class BookCategory //: BaseEntity
{
    public BookCategory(Guid bookId, Guid categoryId)
    {
        BookId = bookId;
        CategoryId = categoryId;
        CreatedAt = DateTime.Now;
    }

    public BookCategory() { }

    public DateTime CreatedAt { get; private set; }
    
    public Guid BookId { get; private set; }
    public Book Book { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    
}
