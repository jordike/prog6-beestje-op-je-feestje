using System.Security.Claims;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Services.Services;

public class BookingService
{
    private readonly BeestjeOpJeFeestjeContext _context;

    public BookingService(BeestjeOpJeFeestjeContext context)
    {
        _context = context;
    }

    public Booking StartBooking(DateOnly date)
    {
        Booking booking = new Booking
        {
            Date = date.ToDateTime(new TimeOnly())
        };
        booking.Animals = new List<Animal>();

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

    public string? StoreSelectedAnimals(int bookingId, List<AnimalViewModel> selectedAnimals, IList<Claim> claims)
    {
        Booking? booking = _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return null;

        _context.Entry(booking).Collection(b => b.Animals).Load();

        var membershipLevelClaim = claims.FirstOrDefault(c => c.Type == "MembershipLevel");
        MembershipLevel membershipLevel = (MembershipLevel)Enum.Parse(typeof(MembershipLevel), membershipLevelClaim.Value);

        int maxAnimals = membershipLevel switch
        {
            MembershipLevel.Geen => 3,
            MembershipLevel.Silver => 4,
            MembershipLevel.Gold => int.MaxValue,
            MembershipLevel.Platinum => int.MaxValue,
            _ => 3
        };

        if (selectedAnimals.Count > maxAnimals)
        {
            return $"Je mag maximaal {maxAnimals} dieren boeken.";
        }

        if (membershipLevel != MembershipLevel.Platinum && selectedAnimals.Any(animal => animal.Animal.Type == AnimalTypes.VIP))
        {
            return "Je mag geen VIP-dieren boeken.";
        }

        booking.Animals = new List<Animal>();

        foreach (AnimalViewModel animalViewModel in selectedAnimals)
        {
            Animal? animal = _context.Animals.Find(animalViewModel.Animal.Id);

            if (animal != null)
                booking.Animals.Add(animal);
        }

        _context.SaveChanges();

        return null;
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
        booking.IsConfirmed = true;
        _context.SaveChanges();
    }

    private bool IsAnimalAvailable(Animal animal, DateTime date)
    {
        return !_context.Bookings
            .Where(booking => booking.Date == date && booking.IsConfirmed)
            .SelectMany(booking => booking.Animals)
            .Contains(animal);
    }
}
