using System.Threading.Tasks;
using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class MusicQuizRepository
    {
        private MusicQuizDbContext Db { get; set; }
        public MusicQuizRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public int AddUser(User user) 
        {
            Db.Users.Add(user);
            return Db.SaveChanges();
        } 

        public bool ExistUser(string username) => Db.Users.Any(u => u.Username == username);

        public User GetUserByUsername(string username) => Db.Users.FirstOrDefault(u => u.Username == username);

        public User GetUserByID(int id) => Db.Users.FirstOrDefault(u => u.ID == id);
    }
}