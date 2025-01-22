using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;
public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
