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

        
    }
}