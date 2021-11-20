using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Dtos
{
    [NotMapped]
    /// <summary> Represents User object as a simplified Read type </summary>
    public class UserSimplifiedReadDto
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public bool IsFriend { get; set; } = false;
    }
}