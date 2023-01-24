using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RestEase;

using SkamBook.Application.Commands.Authentication.RegisterUser;
using SkamBook.Application.Services;
using SkamBook.Infrastructure.Settings;

namespace SkamBook.Application;

public static class DependencyInjection
{
    private const string SETTINGS_SECTION = "Settings";
    
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(SETTINGS_SECTION).Get<ApiSettings>();
        
        services
            .AddSingleton(RestClient.For<IGoogleMapsService>())
            .AddMediatR(typeof(RegisterUserCommand));
        
        return services;
    }
}
