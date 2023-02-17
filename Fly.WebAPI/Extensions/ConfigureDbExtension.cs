using Fly.Data;
using Microsoft.EntityFrameworkCore;

namespace Fly.WebAPI.Extensions;

public static class ConfigureDbExtension
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(DatabaseConfiguration.Configuration).Get<DatabaseConfiguration>();
        services.AddDbContext<FlyDbContext>(
        options =>
        {
            options.UseSqlServer($"Server={cfg.Server},{cfg.Port};Database={cfg.Database};User Id={cfg.User};Password={cfg.Password};MultipleActiveResultSets=true;TrustServerCertificate=true");
        });

        return services;
    }

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(DatabaseConfiguration.Configuration).Get<DatabaseConfiguration>();
        services.AddDbContext<FlyDbContext>(
        options =>
        {
            options.UseNpgsql($"Server={cfg.Server};Port={cfg.Port};Database={cfg.Database};User Id={cfg.User}; Password={cfg.Password};");
        });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}
