using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Dtos
{
    /// <summary> Represents a read model for all users which the current user is set ID in
    /// and have IsAccepted property to false </summary>
    [NotMapped]
    public class FriendshipReadDto
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Username { get; set; }

        public DateTime StartDate { get; set; }

        public bool DidCurrentUserSendRequest { get; set; }
    }
}