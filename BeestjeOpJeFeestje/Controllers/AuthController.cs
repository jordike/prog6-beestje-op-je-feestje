using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels;
using BeestjeOpJeFeestje.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<Account> _userManager;
    private readonly UserStore<Account> _userStore;
    private readonly AuthService _authService;

    public AuthController(UserManager<Account> userManager, UserStore<Account> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
        _authService = new AuthService(userManager, userStore);
    }

    public IActionResult Login()
    {
        LoginViewModel viewModel = new LoginViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        return View(viewModel);
    }
}
