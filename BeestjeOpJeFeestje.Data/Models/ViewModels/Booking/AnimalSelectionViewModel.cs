using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Validations;

namespace BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;

public class AnimalSelectionViewModel
{
    [Required]
    [MustBookAnimalsValidation]
    [AllowedAnimalTypesValidation]
    [NotBookingDesertAnimalsInWinterValidation]
    [NotBookingPenguinsInWeekendValidation]
    [NotBookingSnowAnimalsInSummerValidation]
    public List<AnimalViewModel> Animals { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public int BookingId { get; set; }
}
