namespace BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;

public class BookingOverviewViewModel
{
    public Models.Booking Booking { get; set; }
    public Dictionary<string, int> Discounts { get; set; }
    public int TotalDiscount { get; set; }
    public int Price { get; set; }
}
