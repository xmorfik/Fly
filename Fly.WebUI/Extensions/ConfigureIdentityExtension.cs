namespace Fly.WebUI.Extensions
{
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
                //options.SignedOutCallbackPath = "/home/index";

                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("offline_access");
                options.Scope.Add("api1");
                options.Scope.Add("Role");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy =>
                                  policy.RequireClaim("Role", "Administrator"));
                options.AddPolicy("PassengerOnly", policy =>
                                  policy.RequireClaim("Role", "Passenger"));
                options.AddPolicy("ManagerOnly", policy =>
                                  policy.RequireClaim("Role", "Manager"));
            });
        }
    }
}
