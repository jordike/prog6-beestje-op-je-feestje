using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Services.Validations;

public class MustBookAnimalsValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<Animal>? animals = value as List<Animal>;

        if (animals == null)
            return new ValidationResult("Je moet dieren selecteren om te kunnen boeken.");

        return ValidationResult.Success;
    }
}
