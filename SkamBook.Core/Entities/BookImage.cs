namespace SkamBook.Core.Entities;

public class BookImage //: BaseEntity
{
    public BookImage(Guid bookId, Guid imageId)
    {
        BookId = bookId;
        ImageId = imageId;
        CreatedAt = DateTime.Now;
    }

    public BookImage() { }

    public DateTime CreatedAt { get; private set; }
    
    public Guid BookId { get; private set; }
    public Book Book { get; private set; }
    
    public Guid ImageId { get; private set; }
    public Image Image { get; private set; }
    
}
