using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Validation
{
    public class UserDetailsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value?.ToString() == "simple" || value?.ToString() == "detailed")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The field Type must be simple or detailed.");
        }
    }
}