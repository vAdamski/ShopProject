using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace DuendeIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(
                name: "user",
                userClaims: new[] { JwtClaimTypes.Email }),
            new IdentityResource(
                name: "role",
                userClaims: new[] { JwtClaimTypes.Role }),
            new IdentityResource(
                name: "firstName",
                userClaims: new[] { "firstName" }),
            new IdentityResource(
                name: "lastName",
                userClaims: new[] { "lastName" }),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("api1"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "blazor",
                ClientName = "Client for Blazor use",

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedScopes =
                {
                    "api1", "openid", "user", "role", "profile", IdentityServerConstants.LocalApi.ScopeName,
                    "firstName", "lastName"
                },
                AllowedCorsOrigins = { "https://localhost:7001", "https://idsshopproject.azurewebsites.net" },
                RedirectUris = { "https://localhost:7001/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:7001/" }
            },
            new Client
            {
                ClientId = "swagger",
                ClientName = "Client for Swagger use",

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes =
                {
                    "api1",
                    "openid",
                    "user",
                    "role",
                    IdentityServerConstants.LocalApi.ScopeName,
                    "firstName",
                    "lastName"
                },
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "https://localhost:6001/swagger/oauth2-redirect.html" },
                AllowedCorsOrigins = { "https://localhost:6001", "https://idsshopproject.azurewebsites.net" },
                Enabled = true
            },
            new Client
            {
                ClientId = "mobileapp",
                ClientName = "Client for Mobile App",
                
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = { new Secret("secret".Sha256()) },
                RequirePkce = true,
                RequireClientSecret = false,
                // Add allowed scopes as required
                AllowedScopes =
                {
                    "api1", "openid", "user", "role", "profile",
                    IdentityServerConstants.LocalApi.ScopeName,
                    "firstName", "lastName"
                },
                // Add allowed CORS origins as required
                AllowedCorsOrigins = { "https://idsshopproject.azurewebsites.net", "myapp://callback" },
                RedirectUris = { "myapp://callback" },
                PostLogoutRedirectUris = { "myapp://callback" },
                Enabled = true
            }
        };
}