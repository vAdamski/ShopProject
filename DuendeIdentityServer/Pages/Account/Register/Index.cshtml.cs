using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using DuendeIdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DuendeIdentityServer.Pages.Account.Register;

public class Index : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityServerInteractionService _interaction;

    public Index(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IEventService events,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _interaction = interaction;
    }

    [BindProperty] public RegisterViewModel RegisterVm { get; set; }

    public async Task<IActionResult> OnGetAsync(string returnUrl)
    {
        await BuildModelAsync(returnUrl);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var context = await _interaction.GetAuthorizationContextAsync(RegisterVm.ReturnUrl);

        if (RegisterVm.ButtonAction != "register")
        {
            if (context != null)
            {
                // if the user cancels, send a result back into IdentityServer as if they 
                // denied the consent (even if this client does not require consent).
                // this will send back an access denied OIDC error response to the client.
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage(RegisterVm.ReturnUrl);
                }

                return Redirect(RegisterVm.ReturnUrl);
            }
            else
            {
                // since we don't have a valid context, then we just go back to the home page
                return Redirect("~/");
            }
        }

        if (!ModelState.IsValid)
        {
            await BuildModelAsync(RegisterVm.ReturnUrl);
            return Page();
        }

        var userExist = await _userManager.FindByEmailAsync(RegisterVm.Email);
        if (userExist is not null)
        {
            ModelState.AddModelError("", "Username already exist");
            await BuildModelAsync(RegisterVm.ReturnUrl);
            return Page();
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = RegisterVm.Firstname,
            NormalizedFirstName = RegisterVm.Firstname.Normalize().ToUpper(),
            LastName = RegisterVm.Lastname,
            NormalizedLastName = RegisterVm.Lastname.Normalize().ToUpper(),
            UserName = RegisterVm.Username,
            NormalizedUserName = RegisterVm.Username.Normalize().ToUpper(),
            Email = RegisterVm.Email,
            NormalizedEmail = RegisterVm.Email.Normalize().ToUpper(),
            EmailConfirmed = true,
            PasswordHash = Guid.NewGuid().ToString()
        };

        var createUserResponse = await _userManager.CreateAsync(user, RegisterVm.Password);

        if (!createUserResponse.Succeeded)
        {
            ModelState.AddModelError("", "Something goes wrong");
            await BuildModelAsync(RegisterVm.ReturnUrl);
            return Page();
        }

        return Redirect(RegisterVm.ReturnUrl);
    }

    private async Task BuildModelAsync(string returnUrl)
    {
        RegisterVm = new RegisterViewModel()
        {
            ReturnUrl = returnUrl
        };
    }
}