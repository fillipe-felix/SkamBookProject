namespace SkamBook.Application.ViewModels;

public class BookViewModel
{
    public Guid UserId { get; set; }
    public AddressViewModel Address { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> Images { get; set; }
    public int Distance { get; set; }
    public string DistanceString { get; set; }
}
