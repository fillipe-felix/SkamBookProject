namespace SkamBook.Core.Entities;

public class Address : BaseEntity
{
    public Address(long lat, long lon)
    {
        Lat = lat;
        Lon = lon;
    }

    public long Lat { get; private set; }
    public long Lon { get; private set; }
    public int MaxDistance { get; private set; }
}
