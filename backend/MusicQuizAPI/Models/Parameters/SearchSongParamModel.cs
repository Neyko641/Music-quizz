using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicQuizAPI.Validation;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class SearchSongParamModel
    {
        [Required]
        public string Value { get; set; }

        [SongSearchTypeAttribute]
        public string SearchType { get; set; } = "song-title";
    }
}