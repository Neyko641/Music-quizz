using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicQuizAPI.Validation;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class RandomSongParamModel
    {
        /// <summary> The number of desired songs to get. </summary>
        [Range(1, 100, ErrorMessage = "'count' must be number between 1 and 100!")]
        public int Count { get; set; } = 10;


        /// <summary> The difficulty of the songs. Must be:<br/>
        /// easy, normal or hard.</summary>
        [SongDifficultyAttribute]
        public string Difficulty { get; set; } = "easy";
    }
}