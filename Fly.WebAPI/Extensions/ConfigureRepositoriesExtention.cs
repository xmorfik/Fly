using Fly.Core.Interfaces;
using Fly.Data;

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
