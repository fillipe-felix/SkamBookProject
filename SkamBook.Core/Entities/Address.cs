namespace SkamBook.Core.Entities;

public class Address : BaseEntity
{
    public Address(string lat, string lon, string city)
    {
        Lat = lat;
        Lon = lon;
        City = city;
    }

    public string Lat { get; private set; }
    public string Lon { get; private set; }
    public string City { get; private set; }
    public int MaxDistance { get; private set; }
}
