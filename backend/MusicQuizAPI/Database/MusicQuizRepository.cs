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

        #region User Methods
        public bool ExistUser(string username) => Db.Users.Any(u => u.Username == username);
        public int AddUser(User user) 
        {
            Db.Users.Add(user);
            return Db.SaveChanges();
        }
        public User GetUserByUsername(string username) => Db.Users.FirstOrDefault(u => u.Username == username);
        public User GetUserByID(int id) => Db.Users.FirstOrDefault(u => u.UserID == id);
        #endregion


        #region Anime Methods
        public bool ExistAnime(string title) => Db.Animes.Any(a => a.Title == title);
        public int AddAnime(Anime anime)
        { 
            Db.Animes.Add(anime);
            return Db.SaveChanges();
        }
        public Anime GetAnimeByTitle(string title) => Db.Animes.FirstOrDefault(a => a.Title == title);
        public Anime GetAnimeByID(int id) => Db.Animes.FirstOrDefault(a => a.AnimeID == id);

        #endregion


        #region Song Methods
        public bool ExistSongInAnime(string title) => Db.Animes.Any(a => a.Songs.Any(s => s.Title == title));
        public int AddSong(Song song)
        {
            Db.Songs.Add(song);
            return Db.SaveChanges();
        }
        public Song GetSongByID(int id) => Db.Songs.FirstOrDefault(s => s.SongID == id);
        public IQueryable<Song> GetSongsThatContainsSongTitle(string title) => Db.Songs.Where(s => s.Title.ToLower().Contains(title));
        public IQueryable<Song> GetSongsThatContainsAnimeTitle(string title) => Db.Songs.Where(s => s.Anime.Title.ToLower().Contains(title));
        #endregion


        #region TopAnime Methods
        public bool ExistTopAnime(string title) => Db.TopAnimes.Any(a => a.Title == title);
        public int AddTopAnime(TopAnime anime)
        {
            Db.TopAnimes.Add(anime);
            return Db.SaveChanges();
        }
        public int GetAnimeRank(string title) => Db.TopAnimes.FirstOrDefault(a => a.Title == title).Rank;
        #endregion
        

        public int AnimesCount => Db.Animes.Count();
        public int SongCount => Db.Songs.Count();
    }
}