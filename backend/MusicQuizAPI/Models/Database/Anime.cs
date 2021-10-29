using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class Anime
    {
        [Key]
        public int AnimeID { get; set; }

        [Required]
        public string Title { get; set; }

        public double Score { get; set; } = 0;

        public int Popularity { get; set; } = 0;

        public List<Song> Songs { get; set; }

        public List<FavoriteAnime> Favorites { get; set; }
    }
}