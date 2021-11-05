using System.Collections.Generic;
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

        public bool Exist(int user1Id, int user2Id) 
            => Db.Friendships.Any(fs => (fs.RequestedUserID == user1Id && fs.AcceptedUserID == user2Id) ||
                (fs.RequestedUserID == user2Id && fs.AcceptedUserID == user1Id));

        public int Add(Friendship fs)
        {
            Db.Friendships.Add(fs);
            return Db.SaveChanges();
        }
        
        public int Remove(Friendship fs)
        {
            Db.Friendships.Remove(fs);
            return Db.SaveChanges();
        }

        public int Update(Friendship fs)
        {
            Db.Friendships.Update(fs);
            return Db.SaveChanges();
        }

        public Friendship Get(int user1Id, int user2Id)
            => Db.Friendships.FirstOrDefault(fs => (fs.RequestedUserID == user1Id && fs.AcceptedUserID == user2Id) ||
                (fs.RequestedUserID == user2Id && fs.AcceptedUserID == user1Id));

        public List<User> GetFriends(int id)
            => Db.Friendships.Where(fs => (fs.RequestedUserID == id || fs.AcceptedUserID == id) && fs.IsAccepted)
                .Select(fs => Db.Users.First(u => u.UserID == fs.AcceptedUserID || u.UserID == fs.RequestedUserID))
                .ToList();

        
    }
}