using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Fly.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                {
                    new ApiScope("api1", "Web Api ")
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    new Client
                    {
                        ClientId = "web1",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,
                    
                        // where to redirect to after login
                        RedirectUris = { "https://localhost:5002/signin-oidc" },

                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "https://localhost:5002/home/index" },

                        AllowOfflineAccess = true,

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            "api1"
                        }
                    }
                };
    }
}