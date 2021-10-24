using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class UserRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public UserRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool Exist(string username) => Db.Users.Any(u => u.Username == username);
        
        public int Add(User user) 
        {
            Db.Users.Add(user);
            return Db.SaveChanges();
        }

        public User Get(string username) => 
            Db.Users.FirstOrDefault(u => u.Username == username);

        public User Get(int id) => Db.Users.FirstOrDefault(u => u.UserID == id);
    }
}