using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class FavoriteSong
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int SongID { get; set; }

        public Song Song { get; set; }
        
        public User User { get; set; }
    }
}