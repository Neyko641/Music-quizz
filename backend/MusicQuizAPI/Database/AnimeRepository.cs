using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class AnimeRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public AnimeRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool Exist(string title) => Db.Animes.Any(a => a.Title == title);

        public int Add(Anime anime)
        { 
            Db.Animes.Add(anime);
            return Db.SaveChanges();
        }

        public Anime Get(string title) => Db.Animes.FirstOrDefault(a => a.Title == title);
        
        public Anime Get(int id) => Db.Animes.FirstOrDefault(a => a.AnimeID == id);

        public IQueryable<Anime> GetAllThatContainsTitle(string title) => 
            Db.Animes.Where(a => a.Title.ToLower().Contains(title));

        public int AddScore(int id, int score)
        {
            Anime anime = Db.Animes.First(a => a.AnimeID == id);
            anime.Score += score;
            anime.Score /= anime.Popularity == 1 ? 1 : 2;
            return Db.SaveChanges();
        }

        public int RemoveScore(int id, int score)
        {
            Anime anime = Db.Animes.First(a => a.AnimeID == id);
            int n = anime.Popularity;
            
            if (n == 0) anime.Score = 0;
            else if (n == 1) anime.Score = Db.FavoriteAnimes.First().Score;
            else anime.Score = ((n*anime.Score)-score)/(n-1);
            
            return Db.SaveChanges();
        }

        public int UpdateScore(int id, int newScore, int previousScore)
        {  
            Anime anime = Db.Animes.First(a => a.AnimeID == id);
            int n = anime.Popularity;
            
            if (n == 0) anime.Score = 0;
            else if (n == 1) anime.Score = Db.FavoriteAnimes.First().Score;
            else 
            {
                anime.Score = ((n*anime.Score)-previousScore)/(n-1);
                anime.Score += newScore;
                anime.Score /= 2;
            }

            return Db.SaveChanges();
        }

        public int Count => Db.Animes.Count();
    }
}