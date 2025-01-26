using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using BeestjeOpJeFeestje.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public IActionResult StartBooking(DateOnly date)
    {
        if (date < DateOnly.FromDateTime(DateTime.Now))
        {
            ModelState.AddModelError("Date", "The date cannot be in the past.");

            return RedirectToAction("Index", "Home");
        }

        Booking booking = _bookingService.StartBooking(date);

        return RedirectToAction("SelectAnimals", new
        {
            id = booking.Id
        });
    }

    public IActionResult SelectAnimals(int id)
    {
        Booking? booking = _bookingService.GetBookingById(id);

        if (booking == null)
            return RedirectToAction("Index", "Home");

        List<AnimalViewModel> animalViewModels = _bookingService.GetAnimalViewModels(booking.Date);
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
            return RedirectToAction("SelectAnimals", viewModel.BookingId);

        List<AnimalViewModel> selectedAnimals = viewModel.Animals.Where(animal => animal.IsSelected).ToList();
        _bookingService.StoreSelectedAnimals(viewModel.BookingId, selectedAnimals);

        return RedirectToAction("EnterInformation", new
        {
            id = viewModel.BookingId
        });
    }

    public IActionResult EnterInformation(int id)
    {
        Booking? booking = _bookingService.GetBookingById(id);

        if (booking == null)
            return RedirectToAction("Index", "Home");

        return View(booking);
    }

    [HttpPost]
    public IActionResult StoreInformation(Booking booking)
    {
        _bookingService.StoreInformation(booking);

        return RedirectToAction("BookingOverview", new
        {
            id = booking.Id
        });
    }

    public IActionResult BookingOverview(int id)
    {
        Booking? booking = _bookingService.GetBookingById(id);

        if (booking == null)
            return RedirectToAction("Index", "Home");

        return View(booking);
    }

    [HttpPost]
    public IActionResult BookingConfirmed(Booking booking)
    {
        _bookingService.ConfirmBooking(booking);

        TempData["Success"] = "Uw boeking is geplaatst";

        return RedirectToAction("Index", "Home");
    }
}
