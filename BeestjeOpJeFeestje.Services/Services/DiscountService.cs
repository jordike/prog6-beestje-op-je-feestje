using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Services.Services;

public class DiscountService
{
    public Dictionary<string, int> GetDiscounts(Booking booking)
    {
        Dictionary<string, int> discounts = new Dictionary<string, int>();

        ApplySameTypeDiscount(booking.Animals, discounts);
        ApplyDuckDiscount(booking.Animals, discounts);
        ApplyBookingDayDiscount(discounts, booking.Date);
        ApplyNameLetterDiscount(booking.Animals, discounts);

        if (booking.Account != null)
            ApplyMembershipDiscount(booking.Account, discounts);

        return discounts;
    }

    private void ApplySameTypeDiscount(List<Animal> animals, Dictionary<string, int> discounts)
    {
        IEnumerable<IGrouping<AnimalTypes, Animal>> animalTypeGroups = animals.GroupBy(a => a.Type);

        if (!animalTypeGroups.Any(group => group.Count() >= 3))
            return;

        discounts.Add("3 animals of the same type", 10);
    }

    private void ApplyDuckDiscount(List<Animal> animals, Dictionary<string, int> discounts)
    {
        if (animals.All(a => a.Name != "Duck"))
            return;

        Random random = new Random();

        if (random.Next(1, 7) != 1)
            return;

        discounts["Animal named 'Duck'"] = 50;
    }

    private void ApplyBookingDayDiscount(Dictionary<string, int> discounts, DateTime bookingDate)
    {
        if (bookingDate.DayOfWeek is not (DayOfWeek.Monday or DayOfWeek.Tuesday))
            return;

        discounts["Booking on Monday or Tuesday"] = 15;
    }

    private void ApplyNameLetterDiscount(List<Animal> animals, Dictionary<string, int> discounts)
    {
        foreach (Animal animal in animals)
        {
            int extraDiscount = 0;

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (animal.Name.Contains(c, StringComparison.OrdinalIgnoreCase))
                {
                    extraDiscount += 2;
                }
            }

            if (extraDiscount <= 0)
                continue;

            discounts[$"Animal name with letters 'A', 'B', 'C', etc."] = extraDiscount;
        }
    }

    private void ApplyMembershipDiscount(Account account, Dictionary<string, int> discounts)
    {
        if (account.MembershipLevel == MembershipLevel.Geen)
            return;

        discounts["Membership card"] = 10;
    }
}
