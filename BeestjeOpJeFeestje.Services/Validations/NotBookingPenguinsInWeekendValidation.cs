using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Services.Validations;

public class NotBookingPenguinsInWeekendValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<Animal> animals = value as List<Animal>;

        bool hasPenguin = animals.Any(animal => animal.Type == "Pinguïn");
        bool isWeekend = DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;

        if (hasPenguin && isWeekend)
            return new ValidationResult("Je mag geen pinguïns boeken in het weekend.");

        return ValidationResult.Success;
    }
}
