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
                options.SignedOutCallbackPath = "/home/index";

                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("offline_access");
                options.Scope.Add("api1");
            });
        }
    }
}
