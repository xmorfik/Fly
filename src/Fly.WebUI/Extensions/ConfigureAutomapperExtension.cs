using System.Reflection;

namespace Fly.WebUI.Extensions;

public static class ConfigureAutomapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
