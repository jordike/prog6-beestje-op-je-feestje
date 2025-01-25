using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BeestjesController : Controller
    {
        private readonly BeestjeService _beestjeService;
        public BeestjesController(BeestjeOpJeFeestjeContext context)
        {
            _beestjeService = new BeestjeService(context);
        }

        public IActionResult Index()
        {
            var animals = _beestjeService.GetAnimals();
            return View(animals);
        }

        public IActionResult Create()
        {
            return View(new Animal());
        }

        [HttpPost]
        public IActionResult Create(Animal animal)
        {
            _beestjeService.CreateAnimal(animal);
            return RedirectToAction("Index");
        }

        public IActionResult Read(int id)
        {
            Animal animal = _beestjeService.GetAnimal(id);

            List<Booking>? bookings = _beestjeService.GetAnimalBookings(animal);

            ViewData["bookings"] = bookings;
            return View(animal);
        }

        public IActionResult Update(int id)
        {
            Animal animal = _beestjeService.GetAnimal(id);
            return View(animal);
        }

        [HttpPost]
        public IActionResult Update(Animal animal)
        {
            _beestjeService.UpdateAnimal(animal);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        {
            Animal animal = _beestjeService.GetAnimal(id);
            return View(animal);
        }

        [HttpPost]
        public IActionResult Delete(Animal animal)
        {
            _beestjeService.DeleteAnimal(animal);
            return RedirectToAction("Index");
        }
    }
}
