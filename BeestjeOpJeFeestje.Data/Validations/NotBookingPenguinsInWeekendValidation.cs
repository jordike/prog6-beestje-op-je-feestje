using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;

namespace BeestjeOpJeFeestje.Data.Validations;

public class NotBookingPenguinsInWeekendValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // If no animals are selected, then the user is not booking penguins in the weekend.
        if (value is not List<AnimalViewModel> animals)
            return ValidationResult.Success;

        AnimalSelectionViewModel viewModel = (AnimalSelectionViewModel)validationContext.ObjectInstance;
        bool hasPenguin = animals.Any(animal => animal.Animal.Name == "Pinguïn" && animal.IsSelected);
        bool isWeekend = viewModel.BookingDate.DayOfWeek == DayOfWeek.Saturday || viewModel.BookingDate.DayOfWeek == DayOfWeek.Sunday;

        if (hasPenguin && isWeekend)
            return new ValidationResult("Je mag geen pinguïns boeken in het weekend.");

        return ValidationResult.Success;
    }
}
