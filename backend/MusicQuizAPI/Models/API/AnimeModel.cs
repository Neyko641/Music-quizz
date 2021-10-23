using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.API
{
    [NotMapped]
    public class AnimeModel
    {
        public string uid { get; set; }

        public SongModel song { get; set; }

        public string source { get; set; }

        public string difficulty { get; set; }
    }
}
