using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Models
{
    public static class ModelConverter
    {
        public static object FromSong(Song song)
        {
            return new {
                title = song.Title,
                artist = song.Artist,
                anime_title = song.Anime.Title,
                type = song.SongType,
                type_details = song.DetailedSongType,
                url = song.URL,
                difficulty = song.Difficulty
            };
        }

        public static object FromAnime(Anime a)
        {
            return new {
                title = a.Title
            };
        }
    }
}