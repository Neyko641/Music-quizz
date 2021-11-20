using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Dtos
{
    [NotMapped]
    /// <summary> Represents Song object as a Read type </summary>
    public class SongReadDto
    {
        public int SongID { get; set; }

        public int AnimeID { get; set; }
        
        public string Title { get; set; }

        public string AnimeTitle { get; set; }
        
        public string Artist { get; set; }
        
        public string URL { get; set; }

        public string SongType { get; set; }

        public string DetailedSongType { get; set; }

        public string Difficulty { get; set; }

        public double Score { get; set; } = 0;

        public double UserScore { get; set; } = 0;

        public int Popularity { get; set; } = 0;
    }
}