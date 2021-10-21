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
        public string SongTitle { get; set; }
    }
}