namespace SkamBook.Core.Entities;

public class Image : BaseEntity
{
    public Image()
    {
        
    }
    
    public Image(string urlImage)
    {
        UrlImage = urlImage;
    }
    
    public string UrlImage { get; set; }
    
    public List<BookImage> BookImages { get; private set; }
}
