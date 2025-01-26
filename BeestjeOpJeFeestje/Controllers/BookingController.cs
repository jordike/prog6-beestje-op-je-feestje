using System.Security.Claims;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using BeestjeOpJeFeestje.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;
    private readonly UserManager<Account> _userManager;

    public BookingController(BeestjeOpJeFeestjeContext context, UserManager<Account> userManager)
    {
        _bookingService = new BookingService(context);
        _userManager = userManager;
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
    public async Task<IActionResult> StoreSelectedAnimals(AnimalSelectionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join("; ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return RedirectToAction("SelectAnimals", new
            {
                id = viewModel.BookingId
            });
        }

        List<AnimalViewModel> selectedAnimals = viewModel.Animals.Where(animal => animal.IsSelected).ToList();
        Account? account = await _userManager.GetUserAsync(HttpContext.User);

        string? errorMessage;

        if (account != null)
        {
            IList<Claim> claims = await _userManager.GetClaimsAsync(account);
            errorMessage = _bookingService.StoreSelectedAnimals(viewModel.BookingId, selectedAnimals, claims);
        }
        else
        {
            errorMessage = _bookingService.StoreSelectedAnimals(viewModel.BookingId, selectedAnimals, null);
        }

        if (errorMessage != null)
        {
            TempData["Error"] = errorMessage;

            return RedirectToAction("SelectAnimals", new
            {
                id = viewModel.BookingId
            });
        }

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

        Dictionary<string, int> discounts = _bookingService.GetDiscounts(booking);
        BookingOverviewViewModel viewModel = new BookingOverviewViewModel
        {
            Booking = booking,
            Discounts = discounts
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult BookingConfirmed(BookingOverviewViewModel viewModel)
    {
        _bookingService.ConfirmBooking(viewModel.Booking);

        TempData["Success"] = "Uw boeking is geplaatst";

        return RedirectToAction("Index", "Home");
    }
}
