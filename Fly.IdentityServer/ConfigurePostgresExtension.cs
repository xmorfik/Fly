using Fly.Data;
using Microsoft.EntityFrameworkCore;

namespace Fly.IdentityServer;

public static class ConfigureDbExtension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(PostgresConfiguration.Configuration).Get<PostgresConfiguration>();
        services.AddDbContext<FlyDbContext>(
        options => options.UseNpgsql($"Server={cfg?.Server};Port={cfg?.Port};Database={cfg?.Database};User Id={cfg?.User}; Password={cfg?.Password};"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}
