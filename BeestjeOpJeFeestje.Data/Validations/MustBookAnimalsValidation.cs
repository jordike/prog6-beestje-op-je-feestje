using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models.ViewModels.Booking;
using Microsoft.IdentityModel.Tokens;

namespace BeestjeOpJeFeestje.Data.Validations;

public class MustBookAnimalsValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<AnimalViewModel>? animals = value as List<AnimalViewModel>;

        if (animals == null || animals.All(animal => !animal.IsSelected))
            return new ValidationResult("Je moet dieren selecteren om te kunnen boeken.");

        return ValidationResult.Success;
    }
}
