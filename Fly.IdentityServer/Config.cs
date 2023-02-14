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
                new IdentityResources.Profile(),
                new IdentityResource("Roles", "User role(s)", new List<string> { "Role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                {
                    new ApiScope("api1", "Web Api ", new List<string> { "Roles" })
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    new Client
                    {
                        ClientId = "web1",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,

                        RedirectUris = { "https://localhost:5002/signin-oidc" },
                        FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                        PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                        AllowOfflineAccess = true,

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1",
                            "Roles"
                        },

                        AlwaysIncludeUserClaimsInIdToken = true
                    }
                };
    }
}