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
        

        /// <summary> Search for a friendship where 
        /// <paramref name="user1Id"/> is the one who sends the request
        /// <paramref name="user2Id"/> is the one who accepts it.
        /// </summary>
        public Friendship Get(int user1Id, int user2Id)
            => Db.Friendships.FirstOrDefault(fs => fs.RequestedUserID == user1Id && fs.AcceptedUserID == user2Id);


        public List<Friendship> GetAll()
            => Db.Friendships.ToList();

        
    }
}