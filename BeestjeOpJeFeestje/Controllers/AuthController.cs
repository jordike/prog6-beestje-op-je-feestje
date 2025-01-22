using BeestjeOpJeFeestje.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;
public class AuthController : Controller
{
    public IActionResult Login()
    {
        LoginViewModel viewModel = new LoginViewModel();

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel)
    {
        return View();
    }
}
