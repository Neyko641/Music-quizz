using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models
{
    [NotMapped]
    public class DetailedAnimeModel : AnimeModel
    {
        public string title { get; set; }
        public string file { get; set; }
        public string type { get; set; }
    }
}