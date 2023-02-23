namespace Fly.WebAPI.Extensions;

public static class ConfigureCorsExtension
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(ClientUrl.Configuration).Get<ClientUrl>();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins($"{cfg.Url}"));
        });
    }
}
