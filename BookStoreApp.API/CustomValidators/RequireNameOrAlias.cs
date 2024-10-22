using BookStoreApp.API.Data;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.CustomValidators
{
    public class RequireNameOrAlias : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Get the current model being validated
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();

            // Check if either FirstName/LastName or Alias is filled
            var firstName = type.GetProperty("FirstName")?.GetValue(instance) as string;
            var lastName = type.GetProperty("LastName")?.GetValue(instance) as string;
            var alias = type.GetProperty("Alias").GetValue(instance) as string;

            if((string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)) && string.IsNullOrWhiteSpace(alias))
            {
                return new ValidationResult("Either First Name and Last Name or Alias must be provided.");
            }

            // Validation passed
            return ValidationResult.Success;
        }
    }
}
