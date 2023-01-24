using SkamBook.Application.Extensions;

namespace SkamBook.API.Configurations;

public static class ApiConfigurations
{
    
    
    public static IServiceCollection AddConfigurationsApi(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
        
        return services;
    }
}
