namespace SkamBook.Core.Entities;

public class Address : BaseEntity
{
    

    public long Lat { get; private set; }
    public long Lon { get; private set; }
    public int MaxDistance { get; private set; }
}
