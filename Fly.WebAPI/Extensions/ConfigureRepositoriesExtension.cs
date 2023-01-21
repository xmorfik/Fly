using Fly.Core.Interfaces;
using Fly.Data;

namespace Fly.WebAPI.Extensions;

public static class ConfigureRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        return services;
    }
}
