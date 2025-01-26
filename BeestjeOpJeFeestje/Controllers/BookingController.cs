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
    public IActionResult StartBooking(Booking booking)
    {
        if (booking.Date < DateTime.Now)
        {
            ModelState.AddModelError("Date", "The date cannot be in the past.");

            return RedirectToAction("Index", "Home", booking);
        }

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return RedirectToAction("SelectAnimals", booking);
    }

    public IActionResult SelectAnimals(Booking booking)
    {
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
            Booking = booking
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult StoreSelectedAnimals(AnimalSelectionViewModel viewModel)
    {
        List<AnimalViewModel> selectedAnimals = viewModel.Animals.Where(animal => animal.IsSelected).ToList();
        Booking? booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == viewModel.Booking.Id);

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

        return RedirectToAction("EnterInformation", booking);
    }

    public IActionResult EnterInformation(Booking booking, int? bookingId)
    {
        Booking _booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(_booking => _booking.Id == (bookingId ?? booking.Id));

        if (_booking == null)
            return RedirectToAction("SelectAnimals", booking);

        // Ensure Animals collection is loaded
        _context.Entry(_booking).Collection(b => b.Animals).Load();

        return View(_booking);
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

        return RedirectToAction("BookingOverview", booking);
    }

    public IActionResult BookingOverview(Booking booking)
    {
        Booking? _booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == booking.Id);

        if (_booking == null)
            return RedirectToAction("EnterInformation", booking);

        // Ensure Animals collection is loaded
        _context.Entry(_booking).Collection(b => b.Animals).Load();

        return View(_booking);
    }

    public IActionResult BookingConfirmed(Booking booking)
    {
        Booking? _booking = _context.Bookings.Find(booking.Id);

        if (_booking == null)
            return RedirectToAction("BookingOverview", booking);

        _booking.IsConfirmed = true;
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
