namespace MusicQuizAPI.Models.Dtos
{
    /// <summary> Represents User object as a Read type </summary>
    public class UserReadDto
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public int GuessCount { get; set; } = 0;

        public int PlayCount { get; set; } = 0;

        public string RegisteredDate { get; set; }

        public bool IsFriend { get; set; } = false;
    }
}