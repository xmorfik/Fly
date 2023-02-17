using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Fly.WebUI.Extensions;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookie";
            options.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("Cookie")
        .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = "https://localhost:5004";

            options.ClientId = "web1";
            options.ClientSecret = "secret";
            options.ResponseType = "code";

            options.SaveTokens = true;

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("offline_access");
            options.Scope.Add("api1");
            options.Scope.Add("Roles");

            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapUniqueJsonKey("Roles", "Role");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = "Role"
            };
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
        });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    }
}
