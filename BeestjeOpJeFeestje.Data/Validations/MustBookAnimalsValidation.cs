using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace BeestjeOpJeFeestje.Data.Validations;

public class MustBookAnimalsValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<Animal>? animals = value as List<Animal>;

        if (animals.IsNullOrEmpty())
            return new ValidationResult("Je moet dieren selecteren om te kunnen boeken.");

        return ValidationResult.Success;
    }
}
