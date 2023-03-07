using System.IdentityModel.Tokens.Jwt;

namespace Fly.WebAPI.Extensions;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
         .AddJwtBearer("Bearer", config =>
         {
             config.Authority = "https://localhost:5004";
             config.TokenValidationParameters.ValidateAudience = false;
             config.TokenValidationParameters.ValidateIssuer = true;
         });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdministratorOnly", policy =>
                              policy.RequireClaim("Role", "Administrator"));
            options.AddPolicy("PassengerOnly", policy =>
                              policy.RequireClaim("Role", "Passenger"));
            options.AddPolicy("ManagerOnly", policy =>
                              policy.RequireClaim("Role", "Manager"));
            options.AddPolicy("ManagerAndAdminOnly", policy =>
                              policy.RequireClaim("Role", "Manager", "Administrator"));
            options.AddPolicy("All", policy =>
                              policy.RequireClaim("Role", "Manager", "Administrator", "Passenger"));
        });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


    }
}
