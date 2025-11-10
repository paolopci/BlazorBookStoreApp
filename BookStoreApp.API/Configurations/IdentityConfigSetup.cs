using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApp.API.Configurations;

public static class IdentityConfigSetup
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services
            .AddIdentityCore<ApiUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BookStoreDbContext>()
            .AddDefaultTokenProviders();

        services.AddDataProtection();
        return services;
    }
}
