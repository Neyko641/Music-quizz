namespace MusicQuizAPI.Models.Dtos
{
    /// <summary> Represents Anime object as a Read type </summary>
    public class AnimeReadDto
    {
        public int AnimeID { get; set; }

        public string Title { get; set; }

        public double Score { get; set; } = 0;

        public int UserScore { get; set; } = 0;

        public int Popularity { get; set; } = 0;
    }
}