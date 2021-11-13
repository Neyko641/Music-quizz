using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class AddFavoriteParamModel
    {
        [Required]
        // Hard to believe if we ever have over million users :/
        [Range(0, 1000000, ErrorMessage = "'id' must be positive number!")] 
        public int ID { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "'score' must be number between 1 and 10!")]
        public int Score { get; set; }
    }
}