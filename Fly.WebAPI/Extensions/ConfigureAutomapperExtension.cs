using System.Reflection;

namespace Fly.WebAPI.Extensions;

public static class ConfigureAutomapperExtension
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
