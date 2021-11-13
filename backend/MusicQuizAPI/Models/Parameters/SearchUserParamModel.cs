using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class SearchUserParamModel
    {
        [Required]
        public string Name { get; set; }


        [Range(1, 100, ErrorMessage = "'limit' must be number between 1 and 100!")]
        public int Limit { get; set; } = 10;
    }
}