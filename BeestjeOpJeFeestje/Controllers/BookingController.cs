using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using Microsoft.AspNetCore.Authorization;
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
        Booking? booking = _context.Bookings.Find(viewModel.Booking.Id);

        if (booking == null)
            return RedirectToAction("SelectAnimals", viewModel);

        booking.Animals = new List<Animal>();

        foreach (AnimalViewModel animalViewModel in selectedAnimals)
        {
            Animal? animal = _context.Animals.Find(animalViewModel.Animal.Id);

            if (animal != null)
                booking.Animals.Add(animal);
        }

        _context.SaveChanges();

        return RedirectToAction("EnterInformation", viewModel.Booking);
    }

    public IActionResult EnterInformation(Booking booking)
    {
        return View(booking);
    }

    [HttpPost]
    public IActionResult StoreInformation(Booking booking)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("EnterInformation", booking);

        _context.SaveChanges();

        return RedirectToAction("BookingOverview");
    }

    public IActionResult BookingOverview(Booking booking)
    {
        return View(booking);
    }

    public IActionResult BookingConfirmed(Booking booking)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("BookingOverview", booking);

        booking.IsConfirmed = true;

        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    private bool IsAnimalAvailable(Animal animal, DateTime date)
    {
        return !_context.Bookings
            .Where(booking => booking.Date == date)
            .SelectMany(booking => booking.Animals)
            .Contains(animal);
    }
}
