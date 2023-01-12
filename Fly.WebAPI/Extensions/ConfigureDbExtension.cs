using Fly.Data;
using Microsoft.EntityFrameworkCore;

namespace Fly.WebAPI.Extensions;

public static class ConfigureDbExtension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FlyDbContext>(
        options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
