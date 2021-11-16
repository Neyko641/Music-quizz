namespace MusicQuizAPI.Models.Dtos
{
    /// <summary> Represents User object as a simplified Read type </summary>
    public class UserSimplifiedReadDto
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public bool IsFriend { get; set; } = false;
    }
}