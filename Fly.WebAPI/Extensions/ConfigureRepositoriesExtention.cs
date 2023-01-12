using Ardalis.Specification;
using Fly.Data;

namespace Fly.WebAPI.Extensions;

public static class ConfigureRepositoriesExtention
{
    public static IServiceCollection AddRepositoriesServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepository<>));

        return services;
    }
}
