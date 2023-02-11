using System.Collections;

using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Location.Request;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Interfaces;
using SkamBook.Infrastructure.Settings;

using Address = SkamBook.Core.Entities.Address;
using Location = GoogleApi.Entities.Maps.Geocoding.PlusCode.Request.Location;

namespace SkamBook.Infrastructure.Services;

public class GoogleService : IGoogleService
{
    private readonly GoogleApiSettings _googleApiSettings;

    public GoogleService(IOptions<GoogleApiSettings> googleApiSettings)
    {
        _googleApiSettings = googleApiSettings.Value;
    }

    public async Task<string> GetCityUserByLatLonAsync(string lat, string lon)
    {
        var latDouble = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
        var lonDouble = double.Parse(lon, System.Globalization.CultureInfo.InvariantCulture);
        
        var request = new LocationGeocodeRequest
        {
            Key = _googleApiSettings.ApiKey,
            Location = new Coordinate(latDouble,lonDouble)
        };

        var response = await GoogleMaps.Geocode.LocationGeocode.QueryAsync(request);

        foreach (var responseResult in response.Results)
        {
            foreach (var addressComponent in responseResult.AddressComponents)
            {
                var result = addressComponent.Types.Contains(AddressComponentType.Administrative_Area_Level_2);

                if (result)
                {
                    return addressComponent.ShortName;
                }
            }
        }

        return "";
    }

    public async Task<IList<Element>> FindNearestUsersAsync(string lat, string lon, IEnumerable<Book> books)
    {
        var latDouble = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
        var lonDouble = double.Parse(lon, System.Globalization.CultureInfo.InvariantCulture);
        
        var origin = new CoordinateEx(latDouble, lonDouble);
        var destinations = new List<LocationEx>();
        
        foreach (var boo in books)
        {
            var latDes = double.Parse(boo.User.Address.Lat, System.Globalization.CultureInfo.InvariantCulture);
            var lonDes = double.Parse(boo.User.Address.Lon, System.Globalization.CultureInfo.InvariantCulture);
            destinations.Add(new LocationEx(new CoordinateEx(latDes, lonDes)));
        }

        var request = new DistanceMatrixRequest()
        {
            Key = _googleApiSettings.ApiKey,
            Origins = new[] { new LocationEx(origin) },
            Destinations = destinations
        };
        
        var response = GoogleMaps.DistanceMatrix.QueryAsync(request).Result.Rows;

        var result = new List<Element>();
        
        
        foreach (var row in response)
        {
            foreach (var rowElement in row.Elements)
            {
               result.Add(rowElement);
            }
        }
        
        return result;
    }
}

