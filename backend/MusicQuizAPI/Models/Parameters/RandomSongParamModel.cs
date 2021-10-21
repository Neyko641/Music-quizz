using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicQuizAPI.Validation;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class RandomSongParamModel
    {
        [Range(1, 100)]
        public int Count { get; set; } = 10;

        [SongDifficultyAttribute]
        public string Difficulty { get; set; } = "easy";
    }
}