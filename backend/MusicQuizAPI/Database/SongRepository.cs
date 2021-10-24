using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class SongRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public SongRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool ExistInAnime(string title) => 
            Db.Animes.Any(a => a.Songs.Any(s => s.Title == title));
        
        public int Add(Song song)
        {
            Db.Songs.Add(song);
            return Db.SaveChanges();
        }
        
        public Song Get(int id) => Db.Songs.FirstOrDefault(s => s.SongID == id);
        
        public IQueryable<Song> GetAllThatContainsSongTitle(string title) => 
            Db.Songs.Where(s => s.Title.ToLower().Contains(title));
        
        public IQueryable<Song> GetAllThatContainsAnimeTitle(string title) => 
            Db.Songs.Where(s => s.Anime.Title.ToLower().Contains(title));
        
        public int Count => Db.Songs.Count();
    }
}