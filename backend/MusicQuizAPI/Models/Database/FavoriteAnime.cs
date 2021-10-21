using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class FavoriteAnime
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string AnimeTitle { get; set; }
    }
}