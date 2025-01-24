using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels;
using BeestjeOpJeFeestje.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

[Authorize(Roles = "Admin")]
public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(UserManager<Account> userManager)
    {
        _accountService = new AccountService(userManager);
    }

    public IActionResult Index()
    {
        List<Account> accounts = _accountService.GetAllUsers();

        return View(accounts);
    }

    public async Task<IActionResult> Delete(string id)
    {
        Account? account = await _accountService.GetUserById(id);

        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(string id)
    {
        Account? account = await _accountService.GetUserById(id);

        if (account is null)
            return RedirectToAction("Index");

        await _accountService.DeleteUser(account);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
    {
        Account? account = await _accountService.GetUserById(id);

        return View(account);
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
