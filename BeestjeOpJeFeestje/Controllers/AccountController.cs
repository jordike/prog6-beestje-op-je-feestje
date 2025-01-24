using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels;
using BeestjeOpJeFeestje.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class AccountController : Controller
{
    private AccountService _accountService;

    public AccountController(UserManager<Account> userManager)
    {
        _accountService = new AccountService(userManager);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAccountViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        Tuple<string, IdentityResult> result = await _accountService.CreateUser(viewModel.Name, viewModel.Email, viewModel.Address, viewModel.PhoneNumber, viewModel.MembershipLevel);

        if (!result.Item2.Succeeded)
        {
            TempData["Error"] = string.Join('\n', result.Item2.Errors.Select(error => error.Description));

            return View(viewModel);
        }

        TempData["Password"] = result.Item1;

        return RedirectToAction("Create");
    }
}
