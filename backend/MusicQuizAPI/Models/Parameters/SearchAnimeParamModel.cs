using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicQuizAPI.Validation;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class SearchAnimeParamModel
    {
        [Required]
        public string Title { get; set; }
    }
}