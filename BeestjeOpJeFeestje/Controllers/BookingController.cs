using System.Data.Entity;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class BookingController : Controller
{
    private readonly BeestjeOpJeFeestjeContext _context;

    public BookingController(BeestjeOpJeFeestjeContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult StartBooking(DateOnly date)
    {
        if (date < DateOnly.FromDateTime(DateTime.Now))
        {
            ModelState.AddModelError("Date", "The date cannot be in the past.");

            return RedirectToAction("Index", "Home");
        }

        Booking booking = new Booking
        {
            Date = date.ToDateTime(new TimeOnly())
        };
        booking.Animals = new List<Animal>();

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return RedirectToAction("SelectAnimals", new
        {
            id = booking.Id
        });
    }

    public IActionResult SelectAnimals(int id)
    {
        Booking? booking = _context.Bookings.Find(id);

        if (booking == null)
            return RedirectToAction("Index", "Home");

        List<Animal> animals = _context.Animals.ToList();
        List<AnimalViewModel> animalViewModels = animals
            .Select(animal => new AnimalViewModel
            {
                Animal = animal,
                IsSelected = false,
                IsAvailable = IsAnimalAvailable(animal, booking.Date)
            })
            .ToList();
        AnimalSelectionViewModel viewModel = new AnimalSelectionViewModel
        {
            Animals = animalViewModels,
            BookingDate = booking.Date,
            BookingId = booking.Id
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult StoreSelectedAnimals(AnimalSelectionViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("SelectAnimals", viewModel);

        List<AnimalViewModel> selectedAnimals = viewModel.Animals.Where(animal => animal.IsSelected).ToList();
        Booking? booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == viewModel.BookingId);

        if (booking == null)
            return RedirectToAction("SelectAnimals", viewModel);

        // Ensure Animals collection is loaded
        _context.Entry(booking).Collection(b => b.Animals).Load();

        booking.Animals = new List<Animal>();

        foreach (AnimalViewModel animalViewModel in selectedAnimals)
        {
            Animal? animal = _context.Animals.Find(animalViewModel.Animal.Id);

            if (animal != null)
                booking.Animals.Add(animal);
        }

        _context.SaveChanges();

        return RedirectToAction("EnterInformation", new
        {
            id = booking.Id
        });
    }

    public IActionResult EnterInformation(int id)
    {
        Booking booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(_booking => _booking.Id == id);

        if (booking == null)
            return RedirectToAction("SelectAnimals", booking);

        // Ensure Animals collection is loaded
        _context.Entry(booking).Collection(b => b.Animals).Load();

        return View(booking);
    }

    [HttpPost]
    public IActionResult StoreInformation(Booking booking)
    {
        Booking? _booking = _context.Bookings.Find(booking.Id);

        if (_booking == null)
            return RedirectToAction("SelectAnimals", booking);

        _booking.ContactName = booking.ContactName;
        _booking.ContactEmail = booking.ContactEmail;
        _booking.ContactPhoneNumber = booking.ContactPhoneNumber;
        _booking.ContactAddress = booking.ContactAddress;
        _context.SaveChanges();

        return RedirectToAction("BookingOverview", new
        {
            id = _booking.Id
        });
    }

    public IActionResult BookingOverview(int id)
    {
        Booking? booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == id);

        if (booking == null)
            return RedirectToAction("Index", "Home");

        // Ensure Animals collection is loaded
        _context.Entry(booking).Collection(b => b.Animals).Load();

        return View(booking);
    }

    [HttpPost]
    public IActionResult BookingConfirmed(Booking booking)
    {
        booking.IsConfirmed = true;
        _context.SaveChanges();

        TempData["Success"] = "Uw boeking is geplaatst";

        return RedirectToAction("Index", "Home");
    }

    private bool IsAnimalAvailable(Animal animal, DateTime date)
    {
        return !_context.Bookings
            .Where(booking => booking.Date == date && booking.IsConfirmed)
            .SelectMany(booking => booking.Animals)
            .Contains(animal);
    }
}
