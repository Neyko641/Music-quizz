using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class FavoriteAnimeRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public FavoriteAnimeRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool Exist(int userId, int animeId) => 
            Db.FavoriteAnimes.Any(fa => fa.UserID == userId && fa.AnimeID == animeId);
        
        public int Add(FavoriteAnime fa)
        {
            Db.FavoriteAnimes.Add(fa);
            return Db.SaveChanges();
        }

        public int Remove(FavoriteAnime fa)
        {
            Db.FavoriteAnimes.Remove(fa);
            return Db.SaveChanges();
        }

        public FavoriteAnime Get(int userId, int animeId) => 
            Db.FavoriteAnimes.FirstOrDefault(fa => fa.UserID == userId && fa.AnimeID == animeId);

        public IQueryable<FavoriteAnime> GetAllByUserID(int id) =>
            Db.FavoriteAnimes.Where(fa => fa.UserID == id);
    }
}