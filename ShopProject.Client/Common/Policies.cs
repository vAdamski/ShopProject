using Microsoft.AspNetCore.Authorization;

namespace ShopProject.Client.Common;

public static class Policies
{
    public const string Admin = "Admin";

    public static AuthorizationPolicy AdminRequirements()
    {
        return new AuthorizationPolicyBuilder()
            .RequireClaim("role", "admin")
            .RequireAuthenticatedUser()
            .Build();
    }
}