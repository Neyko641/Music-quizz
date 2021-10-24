using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class FavoriteSongRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public FavoriteSongRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool Exist(int userId, int songId) => 
            Db.FavoriteSongs.Any(fs => fs.UserID == userId && fs.SongID == songId);

        public int Add(FavoriteSong fs)
        {
            Db.FavoriteSongs.Add(fs);
            return Db.SaveChanges();
        }

        public int Remove(FavoriteSong fs)
        {
            Db.FavoriteSongs.Remove(fs);
            return Db.SaveChanges();
        }

        public FavoriteSong Get(int userId, int songId) => 
            Db.FavoriteSongs.FirstOrDefault(fs => fs.UserID == userId && fs.SongID == songId);

        public IQueryable<FavoriteSong> GetAllByUserID(int id) =>
            Db.FavoriteSongs.Where(fa => fa.UserID == id);
    }
}