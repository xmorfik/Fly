using Hangfire;

namespace Fly.WebAPI.Extensions;

public static class ConfigureHangfireExtension
{
    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(DatabaseConfiguration.Configuration).Get<DatabaseConfiguration>();
        services.AddHangfire(x => x.UseSqlServerStorage($"Server={cfg.Server},{cfg.Port};Database={cfg.Database};User Id={cfg.User};Password={cfg.Password};MultipleActiveResultSets=true;TrustServerCertificate=true"));
        services.AddHangfireServer();

        return services;
    }
}
