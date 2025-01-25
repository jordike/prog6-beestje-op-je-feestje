using BeestjeOpJeFeestje.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BeestjesController : Controller
    {
        // TODO: add authorization to this controller to prevent unauthorized access. only Owners of the farm should be able to access this controller.
        public BeestjeOpJeFeestjeContext _context;

        public BeestjesController(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var animals = GetAnimals();
            return View(animals);
        }

        public IActionResult Create()
        {
            return View(new Animal());
        }

        [HttpPost]
        public IActionResult Create(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Read(int id)
        {
            Animal animal = GetAnimal(id);

            List<Booking>? bookings = GetAnimalBookings(animal);

            ViewData["bookings"] = bookings;
            return View(animal);
        }

        public IActionResult Update(int id)
        {
            Animal animal = GetAnimal(id);
            return View(animal);
        }

        [HttpPost]
        public IActionResult Update(Animal animal)
        {
            _context.Animals.Update(animal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        {
            Animal animal = GetAnimal(id);
            return View(animal);
        }

        [HttpPost]
        public IActionResult Delete(Animal animal)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<Animal> GetAnimals() 
        {
            List<Animal> animals = _context.Animals.ToList();
            return animals;
        }

        public Animal GetAnimal(int id)
        {
            Animal animal = _context.Animals.FirstOrDefault(a => a.Id == id);
            return animal;
        }

        public List<Booking> GetAnimalBookings(Animal animal)
        {
            List<Booking> bookings = _context.Bookings.Where(b => b.animals.Contains(animal)).ToList();
            return bookings;
        }
    }
}
