using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.API
{
    [NotMapped]
    public class SongModel
    {
        public string title { get; set; }

        public string artist { get; set; }
    }
}