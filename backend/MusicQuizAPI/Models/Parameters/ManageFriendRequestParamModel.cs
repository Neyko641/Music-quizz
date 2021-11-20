using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class ManageFriendRequestParamModel
    {
        [Required]
        // Hard to believe if we ever have over million users :/
        [Range(0, 1000000, ErrorMessage = "'id' must be positive number!")] 
        public int ID { get; set; }

        [Required]
        public bool IsAccepted { get; set; }
    }
}