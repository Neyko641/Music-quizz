using System;
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
            int result = Db.SaveChanges();
            if (result > 0) IncrementAnimePopularity(fa.AnimeID);
            return result;
        }

        public int Remove(FavoriteAnime fa)
        {
            Db.FavoriteAnimes.Remove(fa);
            int result = Db.SaveChanges();
            if (result > 0) DecrementAnimePopularity(fa.AnimeID);
            return result;
        }

        public int Update(FavoriteAnime fa)
        {
            Db.FavoriteAnimes.Update(fa);
            return Db.SaveChanges();
        }

        public FavoriteAnime Get(int userId, int animeId) => 
            Db.FavoriteAnimes.FirstOrDefault(fa => fa.UserID == userId && fa.AnimeID == animeId);

        public IQueryable<FavoriteAnime> GetAllByUserID(int id) =>
            Db.FavoriteAnimes.Where(fa => fa.UserID == id);

        public void IncrementAnimePopularity(int id)
        {
            Db.Animes.First(a => a.AnimeID == id).Popularity++;
            Db.SaveChanges();
        }

        public void DecrementAnimePopularity(int id)
        {
            Db.Animes.First(a => a.AnimeID == id).Popularity--;
            Db.SaveChanges();
        }
    }
}