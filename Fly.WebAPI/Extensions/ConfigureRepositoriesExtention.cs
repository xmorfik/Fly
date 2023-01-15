using Fly.Data;
using Fly.Data.Interfaces;

namespace Fly.WebAPI.Extensions;

public static class ConfigureRepositoriesExtention
{
    public static IServiceCollection AddRepositoriesServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        return services;
    }
}
