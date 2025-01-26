using System.Security.Claims;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Services.Services;

public class BookingService
{
    private readonly BeestjeOpJeFeestjeContext _context;
    private readonly DiscountService discountService;

    public BookingService(BeestjeOpJeFeestjeContext context)
    {
        _context = context;
        discountService = new DiscountService();
    }

    public Booking StartBooking(DateOnly date)
    {
        Booking booking = new Booking
        {
            Date = date.ToDateTime(new TimeOnly()),
            Animals = []
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return booking;
    }

    public Booking? GetBookingById(int id)
    {
        return _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == id);
    }

    public List<AnimalViewModel> GetAnimalViewModels(DateTime bookingDate)
    {
        List<Animal> animals = _context.Animals.ToList();

        return animals
            .Select(animal => new AnimalViewModel
            {
                Animal = animal,
                IsSelected = false,
                IsAvailable = IsAnimalAvailable(animal, bookingDate)
            })
            .ToList();
    }

    public string? StoreSelectedAnimals(int bookingId, List<AnimalViewModel> selectedAnimals, IList<Claim>? claims)
    {
        Booking? booking = GetBookingByIdWithAnimals(bookingId);

        if (booking == null)
            return null;

        MembershipLevel membershipLevel = claims != null
            ? GetMembershipLevel(claims)
            : MembershipLevel.Geen;
        int maxAnimals = GetMaxAnimalsAllowed(membershipLevel);

        string? validationMessage = ValidateSelectedAnimals(selectedAnimals, membershipLevel, maxAnimals);

        if (validationMessage != null)
            return validationMessage;

        UpdateBookingAnimals(booking, selectedAnimals);
        _context.SaveChanges();

        return null;
    }

    private Booking? GetBookingByIdWithAnimals(int bookingId)
    {
        return _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == bookingId);
    }

    private MembershipLevel GetMembershipLevel(IList<Claim> claims)
    {
        Claim? membershipLevelClaim = claims.FirstOrDefault(c => c.Type == "MembershipLevel");
        return membershipLevelClaim != null
            ? (MembershipLevel) Enum.Parse(typeof(MembershipLevel), membershipLevelClaim.Value)
            : MembershipLevel.Geen;
    }

    private int GetMaxAnimalsAllowed(MembershipLevel membershipLevel)
    {
        return membershipLevel switch
        {
            MembershipLevel.Geen => 3,
            MembershipLevel.Silver => 4,
            MembershipLevel.Gold => int.MaxValue,
            MembershipLevel.Platinum => int.MaxValue,
            _ => 3
        };
    }

    private string? ValidateSelectedAnimals(List<AnimalViewModel> selectedAnimals, MembershipLevel membershipLevel, int maxAnimals)
    {
        if (selectedAnimals.Count > maxAnimals)
        {
            return $"Je mag maximaal {maxAnimals} dieren boeken.";
        }

        if (membershipLevel != MembershipLevel.Platinum && selectedAnimals.Any(animal => animal.Animal.Type == AnimalTypes.VIP))
        {
            return "Je mag geen VIP-dieren boeken.";
        }

        return null;
    }

    private void UpdateBookingAnimals(Booking booking, List<AnimalViewModel> selectedAnimals)
    {
        booking.Animals = new List<Animal>();

        foreach (Animal? animal in selectedAnimals.Select(animalViewModel => _context.Animals.Find(animalViewModel.Animal.Id)).OfType<Animal>())
        {
            booking.Animals.Add(animal);
        }
    }

    public void StoreInformation(Booking booking)
    {
        Booking? _booking = _context.Bookings.Find(booking.Id);

        if (_booking == null)
            return;

        _booking.ContactName = booking.ContactName;
        _booking.ContactEmail = booking.ContactEmail;
        _booking.ContactPhoneNumber = booking.ContactPhoneNumber;
        _booking.ContactAddress = booking.ContactAddress;
        _context.SaveChanges();
    }

    public void ConfirmBooking(Booking booking)
    {
        Booking? _booking = _context.Bookings.Find(booking.Id);

        if (_booking == null)
            return;

        _booking.IsConfirmed = true;
        _context.SaveChanges();
    }

    public Dictionary<string, int> GetDiscounts(Booking booking)
    {
        return discountService.GetDiscounts(booking);
    }

    private bool IsAnimalAvailable(Animal animal, DateTime date)
    {
        return !_context.Bookings
            .Where(booking => booking.Date == date && booking.IsConfirmed)
            .SelectMany(booking => booking.Animals)
            .Contains(animal);
    }
}
