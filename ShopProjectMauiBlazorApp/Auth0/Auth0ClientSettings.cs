namespace ShopProjectMauiBlazorApp.Auth0;

public static class Auth0ClientSettings
{
    public static Auth0ClientOptions OwnIds() => new Auth0ClientOptions()
    {
        Domain = "idsshopproject.azurewebsites.net",
        ClientId = "mobileapp",
        Scope = "openid profile user firstName lastName IdentityServerApi api1",
        RedirectUri = "myapp://callback",
    };
    
    public static Auth0ClientOptions Auth0Ids() => new Auth0ClientOptions()
    {
        Domain = "dev-u8i8ssxpwy2t06gm.us.auth0.com",
        ClientId = "Vxa9egkZGTQzdvYhmxL5Bxhjp9QbUPlk",
        Scope = "openid",
        RedirectUri = "myapp://callback",
    };
}