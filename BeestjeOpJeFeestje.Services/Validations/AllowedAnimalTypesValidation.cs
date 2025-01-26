using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Services.Validations;

public class AllowedAnimalTypesValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        List<Animal> animals = value as List<Animal>;

        bool hasFarmAnimal = animals.Any(animal => animal.Type == "Boerderijdier");
        bool hasRestrictedAnimal = animals.Any(animal => animal.Type is "Leeuw" or "IJsbeer");

        if (hasFarmAnimal && hasRestrictedAnimal)
            return new ValidationResult("Je mag geen beestje boeken van het type 'Leeuw' of 'IJsbeer' als je ook een beestje boekt van het type 'Boerderijdier'.");

        return ValidationResult.Success;
    }
}
