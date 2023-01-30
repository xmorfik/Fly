using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fly.WebAPI.Extensions;

public static class ConfigureJwt
{
    public static void ConfigureJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(JwtSettings.Settings).Get<JwtSettings>();
        var secretKey = configuration.GetSection("Secret:Key").Value;
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = cfg.ValidIssuer,
                ValidAudience = cfg.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }
}
