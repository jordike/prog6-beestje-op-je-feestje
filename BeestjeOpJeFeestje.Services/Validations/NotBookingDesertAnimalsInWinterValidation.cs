using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Services.Validations;

public class NotBookingDesertAnimalsInWinterValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<Animal> animals = new List<Animal>();

        bool hasDesertAnimal = animals.Any(animal => animal.Type == "Woestijn");
        bool isWinter = DateTime.Now.Month >= 10 || DateTime.Now.Month <= 2;

        if (hasDesertAnimal && isWinter)
            return new ValidationResult("Je mag geen woestijndieren boeken in de winter.");

        return ValidationResult.Success;
    }
}
