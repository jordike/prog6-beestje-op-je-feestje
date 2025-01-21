using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BeestjesController : Controller
    {
        // TODO: add authorization to this controller to prevent unauthorized access. only Owners of the farm should be able to access this controller.
        public IActionResult Index()
        {
            return View();
        }
    }
}
