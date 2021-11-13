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

        public bool Exist(FavoriteSong fs) => 
            Db.FavoriteSongs.Any(s => s.UserID == fs.UserID && s.SongID == fs.SongID);

        public int Add(FavoriteSong fs)
        {
            Db.FavoriteSongs.Add(fs);
            int result = Db.SaveChanges();
            if (result > 0) IncrementSongPopularity(fs.SongID);
            return result;
        }

        public int Remove(FavoriteSong fs)
        {
            Db.FavoriteSongs.Remove(fs);
            int result = Db.SaveChanges();
            if (result > 0) DecrementSongPopularity(fs.SongID);
            return result;
        }

        public int Update(FavoriteSong fs)
        {
            Db.FavoriteSongs.Update(fs);
            return Db.SaveChanges();
        }

        public FavoriteSong Get(int userId, int songId) => 
            Db.FavoriteSongs.FirstOrDefault(fs => fs.UserID == userId && fs.SongID == songId);

        public IQueryable<FavoriteSong> GetAllByUserID(int id) =>
            Db.FavoriteSongs.Where(fa => fa.UserID == id);

        public void IncrementSongPopularity(int id)
        {
            Db.Songs.First(s => s.SongID == id).Popularity++;
            Db.SaveChanges();
        }

        public void DecrementSongPopularity(int id)
        {
            Db.Songs.First(s => s.SongID == id).Popularity--;
            Db.SaveChanges();
        }
    }
}