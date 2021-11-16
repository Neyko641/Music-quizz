using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicQuizAPI.Validation;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class SearchUserParamModel
    {
        [Required]
        public string Name { get; set; }


        [Range(1, 100, ErrorMessage = "'limit' must be number between 1 and 100!")]
        public int Limit { get; set; } = 10;


        /// <summary> Represents how much details should the user have. Must be:<br/>
        /// detailed or simple.</summary>
        [UserDetailsAttribute]
        public string Type { get; set; } = "simple";
    }
}