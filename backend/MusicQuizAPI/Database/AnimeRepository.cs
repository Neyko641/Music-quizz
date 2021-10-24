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

        public int Count => Db.Animes.Count();
    }
}