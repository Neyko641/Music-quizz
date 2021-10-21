using Microsoft.EntityFrameworkCore;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class MusicQuizDbContext : DbContext
    {
        public MusicQuizDbContext(DbContextOptions<MusicQuizDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteAnime> FavoriteAnimes { get; set; }
        public DbSet<FavoriteSong> FavoriteSongs { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
    }
}