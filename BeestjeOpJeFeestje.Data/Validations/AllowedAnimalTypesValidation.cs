using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Validations;

public class AllowedAnimalTypesValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // If there are no animals, then the user is not booking animals with conflicting types.
        if (value is not List<Animal> animals)
            return ValidationResult.Success;

        bool hasFarmAnimal = animals.Any(animal => animal.Type == AnimalTypes.Boerderij);
        bool hasRestrictedAnimal = animals.Any(animal => animal.Name is "Leeuw" or "IJsbeer");

        if (hasFarmAnimal && hasRestrictedAnimal)
            return new ValidationResult("Je mag geen beestje boeken van het type 'Leeuw' of 'IJsbeer' als je ook een beestje boekt van het type 'Boerderijdier'.");

        return ValidationResult.Success;
    }
}
