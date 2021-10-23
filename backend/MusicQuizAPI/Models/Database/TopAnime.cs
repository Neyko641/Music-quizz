using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class TopAnime
    {
        [Key]
        public int TopAnimeID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Rank { get; set; }
    }
}