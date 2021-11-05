using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Models
{
    public static class ModelConverter
    {
        public static object FromSong(Song song, int userScore = 0)
        {
            return new {
                id = song.SongID,
                title = song.Title,
                artist = song.Artist,
                anime_title = song.Anime.Title,
                type = song.SongType,
                type_details = song.DetailedSongType,
                url = song.URL,
                difficulty = song.Difficulty,
                score = song.Score,
                user_score = userScore,
                popularity = song.Popularity,
            };
        }

        public static object FromAnime(Anime anime, int userScore = 0)
        {
            return new {
                id = anime.AnimeID,
                title = anime.Title,
                score = anime.Score,
                user_score = userScore,
                popular = anime.Popularity,
            };
        }

        public static object FromUser(User user)
        {
            return new {
                id = user.UserID,
                username = user.Username,
                avatar = user.Avatar,
                guess_count = user.GuessCount,
                played_count = user.PlayCount,
                registered_date = user.RegisteredDate.ToString("d MMM yyyy"),
                is_friend = user.IsFriend
            };
        }
    }
}