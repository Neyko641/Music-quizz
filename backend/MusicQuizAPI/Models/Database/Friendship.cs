using System;
using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class Friendship
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int RequestedUserID { get; set; }

        [Required]
        public int AcceptedUserID { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public bool IsAccepted { get; set; } = false;
    }
}