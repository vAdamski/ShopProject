using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using DuendeIdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace DuendeIdentityServer.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userMgr;

    public ProfileService(UserManager<ApplicationUser> userMgr)
    {
        _userMgr = userMgr;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userMgr.GetUserAsync(context.Subject);


        var claims = new List<Claim>()
        {
            new Claim("Email", user.Email),
            new Claim("name", user.UserName),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName)
        };

        var roles = await _userMgr.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(JwtClaimTypes.Role, role));
        }

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userMgr.GetUserAsync(context.Subject);
        context.IsActive = user != null;
    }
}