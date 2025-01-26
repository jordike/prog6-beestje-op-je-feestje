using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels;
using BeestjeOpJeFeestje.Services.Services;
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
        if (!ModelState.IsValid)
            return View(viewModel);

        if (!await _authService.Login(viewModel.Email, viewModel.Password))
        {
            TempData["Error"] = "Het e-mailadres of wachtwoord is onjuist.";

            return View(viewModel);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();

        return RedirectToAction("Index", "Home");
    }
}
