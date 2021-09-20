namespace MusicQuizAPI.Models
{
    public class AnimeModel
    {
        public string uid { get; set; }

        public SongModel song { get; set; }

        public string source { get; set; }

        public string difficulty { get; set; }
    }
}
