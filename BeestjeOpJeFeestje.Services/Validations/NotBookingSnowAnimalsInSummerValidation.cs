using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Services.Validations;

public class NotBookingSnowAnimalsInSummerValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<Animal> animals = value as List<Animal>;

        bool hasSnowAnimal = animals.Any(animal => animal.Type == "Sneeuw");
        bool isSummer = DateTime.Now.Month >= 6 && DateTime.Now.Month <= 8;

        if (hasSnowAnimal && isSummer)
            return new ValidationResult("Je mag geen sneeuwdieren boeken in de zomer.");

        return ValidationResult.Success;
    }
}
