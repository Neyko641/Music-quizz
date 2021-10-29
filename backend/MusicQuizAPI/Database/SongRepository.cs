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

        public bool ExistInAnime(string title) 
            => Db.Animes.Any(a => a.Songs.Any(s => s.Title == title));
        
        public int Add(Song song)
        {
            Db.Songs.Add(song);
            return Db.SaveChanges();
        }
        
        public Song Get(int id) => Db.Songs.FirstOrDefault(s => s.SongID == id);
        
        public IQueryable<Song> GetAllThatContainsSongTitle(string title) 
            => Db.Songs.Where(s => s.Title.ToLower().Contains(title));
        
        public IQueryable<Song> GetAllThatContainsAnimeTitle(string title) 
            => Db.Songs.Where(s => s.Anime.Title.ToLower().Contains(title));

        public int AddScore(int id, int score)
        {
            Song song = Db.Songs.First(s => s.SongID == id);
            song.Score += score;
            song.Score /= song.Popularity == 1 ? 1 : 2;
            return Db.SaveChanges();
        }

        public int RemoveScore(int id, int score)
        {
            Song song = Db.Songs.First(s => s.SongID == id);
            int n = song.Popularity;
            
            if (n == 0) song.Score = 0;
            else if (n == 1) song.Score = Db.FavoriteSongs.First().Score;
            else song.Score = ((n*song.Score)-score)/(n-1);
            
            return Db.SaveChanges();
        }

        public int UpdateScore(int id, int newScore, int previousScore)
        {  
            Song song = Db.Songs.First(s => s.SongID == id);
            int n = song.Popularity;
            
            if (n == 0) song.Score = 0;
            else if (n == 1) song.Score = Db.FavoriteAnimes.First().Score;
            else 
            {
                song.Score = ((n*song.Score)-previousScore)/(n-1);
                song.Score += newScore;
                song.Score /= 2;
            }

            return Db.SaveChanges();
        }
        
        public int Count => Db.Songs.Count();
    }
}