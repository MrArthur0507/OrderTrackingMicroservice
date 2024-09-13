using Duende.IdentityServer.Models;

namespace OderTrackingIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            //only for development and testing purposes
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile"},
                RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                ClientSecrets = new[] {new Secret("testSecret".Sha256())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
            new Client
            {
                ClientId = "angular",
                ClientName = "Angular",
                AllowedScopes = {"openid", "profile"},
                RedirectUris = {"https://localhost:4200/signin-oidc"},
                ClientSecrets = new[] {new Secret("testSecret".Sha256())},
                AllowedGrantTypes = {GrantType.AuthorizationCode},
                
            }
        };
}
