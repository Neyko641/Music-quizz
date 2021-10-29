using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class Song
    {
        [Key]
        public int SongID { get; set; }

        public int AnimeID { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string Artist { get; set; }
        
        public string URL { get; set; }

        public string SongType { get; set; }

        public string DetailedSongType { get; set; }

        public string Difficulty { get; set; }

        public double Score { get; set; } = 0;

        public int Popularity { get; set; } = 0;

        public Anime Anime { get; set; }

        public List<FavoriteSong> Favorites { get; set; }
    }
}