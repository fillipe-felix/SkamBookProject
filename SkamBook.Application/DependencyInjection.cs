using MediatR;

using Microsoft.Extensions.DependencyInjection;

using SkamBook.Application.Commands.Authentication.RegisterUser;

namespace SkamBook.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(RegisterUserCommand));
        
        return services;
    }
}
