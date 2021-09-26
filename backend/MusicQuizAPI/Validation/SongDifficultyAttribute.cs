using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Validation
{
    public class SongDifficultyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!(value?.ToString() == "easy" || value?.ToString() == "medium" || value?.ToString() == "hard"))
            {
                return new ValidationResult($"The field Difficulty must be easy, medium or hard.");   
            }

            return ValidationResult.Success;
        }
    }
}