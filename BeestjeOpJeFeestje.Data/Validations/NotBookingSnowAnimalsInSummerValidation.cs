using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;

namespace BeestjeOpJeFeestje.Data.Validations;

public class NotBookingSnowAnimalsInSummerValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // If there are no animals, then the user is not booking snow animals in the summer.
        if (value is not List<AnimalViewModel> animals)
            return ValidationResult.Success;

        bool hasSnowAnimal = animals.Any(animal => animal.Animal.Type == AnimalTypes.Sneeuw && animal.IsSelected);
        bool isSummer = DateTime.Now.Month >= 6 && DateTime.Now.Month <= 8;

        if (hasSnowAnimal && isSummer)
            return new ValidationResult("Je mag geen sneeuwdieren boeken in de zomer.");

        return ValidationResult.Success;
    }
}
