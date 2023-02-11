using GoogleApi.Entities.Maps.DistanceMatrix.Response;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Interfaces;

public interface IGoogleService
{
    Task<string> GetCityUserByLatLonAsync(string lat, string lon);
    
    Task<IList<Element>> FindNearestUsersAsync(string lat, string lon, IEnumerable<Book> users);
}
