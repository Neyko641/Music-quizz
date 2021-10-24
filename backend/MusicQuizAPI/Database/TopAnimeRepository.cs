using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class TopAnimeRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public TopAnimeRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool Exist(string title) => Db.TopAnimes.Any(a => a.Title == title);
        
        public int Add(TopAnime anime)
        {
            Db.TopAnimes.Add(anime);
            return Db.SaveChanges();
        }
        
        public TopAnime Get(string title) => 
            Db.TopAnimes.FirstOrDefault(a => a.Title == title);
    }
}