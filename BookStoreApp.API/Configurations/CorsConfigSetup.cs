using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApp.API.Configurations;

public static class CorsConfigSetup
{
    private const string AllowAllPolicyName = "AllowAll";

    public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowAllPolicyName, builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });

        return services;
    }

    public static string GetAllowAllPolicyName() => AllowAllPolicyName;
}
