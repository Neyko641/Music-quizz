using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class FriendshipRepository
    {
        private MusicQuizDbContext Db { get; set; }

        public FriendshipRepository(MusicQuizDbContext db)
        {
            Db = db;
        }


    }
}