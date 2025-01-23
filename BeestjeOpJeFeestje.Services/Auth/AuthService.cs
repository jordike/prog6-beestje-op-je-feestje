using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Services.Auth;

public class AuthService
{
    private readonly UserManager<Account> _userManager;
    private readonly UserStore<Account> _userStore;

    public AuthService(UserManager<Account> userManager, UserStore<Account> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
    }

    public void Login(string email, string password)
    {
        
    }

    public void Logout()
    {

    }

    public void CreateUser(string email, string password)
    {

    }
}
