using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BeestjeOpJeFeestje.Services.Services;

public class AuthService
{
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;

    public AuthService(UserManager<Account> userManager, SignInManager<Account> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Login(string email, string password)
    {
        Account? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return false;

        SignInResult result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim("MembershipLevel", user.MembershipLevel.ToString()));
        }

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
