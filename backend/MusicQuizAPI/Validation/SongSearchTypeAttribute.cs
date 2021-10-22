using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Validation
{
    public class SongSearchTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!(value?.ToString() == "anime-title" || value?.ToString() == "song-title"))
            {
                return new ValidationResult($"The field SearchType must be 'anime-title' or 'song-title'.");   
            }

            return ValidationResult.Success;
        }
    }
}