using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Models
{
    public static class ModelConverter
    {
        public static object FromSong(Song song)
        {
            return new {
                id = song.SongID,
                title = song.Title,
                artist = song.Artist,
                anime_title = song.Anime.Title,
                type = song.SongType,
                type_details = song.DetailedSongType,
                url = song.URL,
                difficulty = song.Difficulty
            };
        }

        public static object FromAnime(Anime anime)
        {
            return new {
                id = anime.AnimeID,
                title = anime.Title
            };
        }
    }
}