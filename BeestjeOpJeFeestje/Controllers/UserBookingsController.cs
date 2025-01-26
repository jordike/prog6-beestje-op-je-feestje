using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using BeestjeOpJeFeestje.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers;

public class UserBookingsController : Controller
{
    private readonly BookingService _bookingService;
    private readonly UserManager<Account> _userManager;

    public UserBookingsController(BeestjeOpJeFeestjeContext context, UserManager<Account> userManager)
    {
        _bookingService = new BookingService(context);
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        Account? user = await _userManager.GetUserAsync(User);

        if (user == null)
            return RedirectToAction("Index", "Home");

        List<Booking> bookings = _bookingService.GetAllUserBookings(user);
        List<ViewOwnBookingViewModel> viewModels = bookings
            .Select(booking => new ViewOwnBookingViewModel
            {
                Booking = booking,
                TotalDiscount = _bookingService.GetTotalDiscount(_bookingService.GetDiscounts(booking)),
                Discounts = _bookingService.GetDiscounts(booking),
                Price = _bookingService.GetPriceAfterDiscounts(booking)
            })
            .ToList();

        return View(viewModels);
    }

    public IActionResult View(int id)
    {
        Booking? booking = _bookingService.GetBookingById(id);

        if (booking == null)
            return RedirectToAction("Index");

        ViewOwnBookingViewModel viewModel = new ViewOwnBookingViewModel
        {
            Booking = booking,
            TotalDiscount = _bookingService.GetTotalDiscount(_bookingService.GetDiscounts(booking)),
            Discounts = _bookingService.GetDiscounts(booking),
            Price = _bookingService.GetPriceAfterDiscounts(booking)
        };

        return View(viewModel);
    }

    public IActionResult Delete(int id)
    {
        Booking? booking = _bookingService.GetBookingById(id);

        if (booking == null)
            return RedirectToAction("Index");

        return View(booking);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        Booking? booking = _bookingService.GetBookingById(id);

        if (booking != null)
            _bookingService.DeleteBooking(booking);

        return RedirectToAction("Index");
    }
}
