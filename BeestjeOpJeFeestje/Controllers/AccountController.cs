using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
