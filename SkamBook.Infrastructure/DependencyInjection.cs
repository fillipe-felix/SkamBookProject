using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;
using SkamBook.Infrastructure.Interfaces;
using SkamBook.Infrastructure.Repositories;
using SkamBook.Infrastructure.Services;
using SkamBook.Infrastructure.Settings;
using SkamBook.Infrastructure.UoW;

namespace SkamBook.Infrastructure;

public static class DependencyInjection
{
    private const string SETTINGS_SECTION = "Settings";
    private const string CS_AUTHENTICATION = "Authentication";
    private const string CS_SKAMBOOK = "Skambook";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(SETTINGS_SECTION).Get<ApiSettings>();

        services.AddDbContext<SkamBookContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString(CS_SKAMBOOK)));
        
        services.AddDbContext<AuthenticationDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString(CS_AUTHENTICATION)));
        
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }) // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = settings.JwtSettings.ValidAudience,
                    ValidIssuer = settings.JwtSettings.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.Secret))
                };
            });

        
        
        services
            .AddSingleton(settings)
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IImageRepository, ImageRepository>()
            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IAzureService, AzureService>()
            .AddScoped<IEmailService, SendGridService>()
            .AddScoped<ITokenPasswordRepository, TokenPasswordRepository>();

        CreateRoles(builder.Services.BuildServiceProvider());
        
        return services;
    }
    
    private static void CreateRoles(IServiceProvider serviceProvider)
    {

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        Task<IdentityResult> roleResult;
        string email = "fillipe.admin@teste.com";

        //Check that there is an Administrator role and create if not
        Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Admin");
        hasAdminRole.Wait();

        if (!hasAdminRole.Result)
        {
            roleResult = roleManager.CreateAsync(new IdentityRole("Admin"));
            roleResult.Wait();
        }

        //Check if the admin user exists and create it if not
        //Add to the Administrator role

        Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
        testUser.Wait();

        if (testUser.Result == null)
        {
            ApplicationUser administrator = new ApplicationUser();
            administrator.Email = email;
            administrator.UserName = email;

            Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "Teste@123");
            newUser.Wait();

            if (newUser.Result.Succeeded)
            {
                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Admin");
                newUserRole.Wait();
            }
        }

    }
}
