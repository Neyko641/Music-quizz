using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Database
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        [Required]
        public int GuessCount { get; set; } = 0;

        [Required]
        public int PlayCount { get; set; } = 0;

        [Required]
        public DateTime RegisteredDate { get; set; }

        public List<FavoriteAnime> Favorites { get; set; }

        [NotMapped]
        public bool IsFriend { get; set; } = false;
    }
}