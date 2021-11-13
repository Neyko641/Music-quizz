using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicQuizAPI.Validation;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class SearchSongParamModel
    {
        [Required]
        public string Title { get; set; }


        /// <summary> Must be: 'song-title' or 'anime-title'. </summary>
        [SongSearchTypeAttribute]
        public string SearchType { get; set; } = "song-title";
    }
}