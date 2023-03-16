using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Fly.Core;

namespace Fly.IdentityServer;

public static class Config
{
    public static ClientUriConfiguration ClientUriConfiguration { get; set; }
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(Scopes.Roles, "User role(s)", new List<string> { Scopes.Role }),
            new IdentityResource(Claims.Airline, "Airline", new List<string> { Claims.Airline })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope("api1", "Web Api ", new List<string> { Scopes.Role,  Claims.Airline })
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client
                {
                    ClientId = "web1",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { ClientUriConfiguration.Uri + "/signin-oidc" },
                    FrontChannelLogoutUri = ClientUriConfiguration.Uri,
                    PostLogoutRedirectUris = { ClientUriConfiguration.Uri + "/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        Scopes.Roles,
                        Claims.Airline
                    },

                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
}