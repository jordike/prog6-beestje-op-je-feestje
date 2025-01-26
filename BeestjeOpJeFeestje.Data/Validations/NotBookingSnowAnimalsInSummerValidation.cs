using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Validations;

public class NotBookingSnowAnimalsInSummerValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // If there are no animals, then the user is not booking snow animals in the summer.
        if (value is not List<Animal> animals)
            return ValidationResult.Success;

        bool hasSnowAnimal = animals.Any(animal => animal.Type == AnimalTypes.Sneeuw);
        bool isSummer = DateTime.Now.Month >= 6 && DateTime.Now.Month <= 8;

        if (hasSnowAnimal && isSummer)
            return new ValidationResult("Je mag geen sneeuwdieren boeken in de zomer.");

        return ValidationResult.Success;
    }
}
