namespace Fly.WebAPI.Extensions
{
    public static class ConfigureIdentityExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", config =>
             {
                 config.Authority = "https://localhost:5004";
                 config.TokenValidationParameters.ValidateAudience = false;
             });
        }
    }
}
