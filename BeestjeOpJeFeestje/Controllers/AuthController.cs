using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels;
using BeestjeOpJeFeestje.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

[Authorize]
public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(UserManager<Account> userManager, SignInManager<Account> signInManager)
    {
        _authService = new AuthService(userManager, signInManager);
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        LoginViewModel viewModel = new LoginViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (ModelState.IsValid && await _authService.Login(viewModel.Email, viewModel.Password))
            return RedirectToAction("Index", "Home");

        return View(viewModel);
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAccount(CreateAccountViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        string? password = await _authService.CreateUser(viewModel.Name, viewModel.Email, viewModel.Address, viewModel.PhoneNumber, Membershiplevel.Silver);

        if (password == null)
            return View(viewModel);

        TempData["Password"] = password;

        return RedirectToAction("CreateAccount");
    }
}
