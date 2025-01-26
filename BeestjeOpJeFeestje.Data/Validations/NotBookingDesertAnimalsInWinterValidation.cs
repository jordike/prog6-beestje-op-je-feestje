using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;

namespace BeestjeOpJeFeestje.Data.Validations;

public class NotBookingDesertAnimalsInWinterValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // If there are no animals, then the user is not booking desert animals in winter.
        if (value is not List<AnimalViewModel> animals)
            return ValidationResult.Success;

        bool hasDesertAnimal = animals.Any(animal => animal.Animal.Type == AnimalTypes.Woestijn);
        bool isWinter = DateTime.Now.Month >= 10 || DateTime.Now.Month <= 2;

        if (hasDesertAnimal && isWinter)
            return new ValidationResult("Je mag geen woestijndieren boeken in de winter.");

        return ValidationResult.Success;
    }
}
