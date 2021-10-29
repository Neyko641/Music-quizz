using System.Linq;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;

namespace MusicQuizAPI.Services
{
    public class FavoriteSongService
    {
        private readonly ILogger<FavoriteAnimeService> _logger;
        private readonly SongRepository _songRepo;
        private readonly FavoriteSongRepository _favSongRepo;
        private readonly AnimeRepository _animeRepo;
        private readonly int _songCount;

        public FavoriteSongService(ILogger<FavoriteAnimeService> logger, SongRepository songRepo,
            FavoriteSongRepository favSongRepo, AnimeRepository animeRepo)
        {
            _logger = logger;
            _songRepo = songRepo;
            _favSongRepo = favSongRepo;
            _songCount = _songRepo.Count;
            _animeRepo = animeRepo;
        }

        public ResultContext AddFavoriteSong(User user, int id, int score)
        {
            ResultContext result = new ResultContext();

            if (id > 0 && id <= _songCount && score > 0 && score <= 10)
            {
                if (!_favSongRepo.Exist(user.UserID, id))
                {
                    FavoriteSong fs = new FavoriteSong
                    {
                        UserID = user.UserID,
                        SongID = id,
                        Score = score
                    };

                    if (_favSongRepo.Add(fs) > 0)
                    {
                        // Updates the average score of the song
                        _songRepo.AddScore(id, score);

                        result.AddData($"The song with id of [{id}] " +
                        $"was added successfully to the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot add song with id of [{id}] in favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The song with id of [{id}] " + 
                    $"is already in favorites for user {user.Username} or doesn't exist!");
            }
            else result.AddExceptionMessage($"'id' parameter must be positive number and " + 
                $"'score' must be between 1 and 10!");

            return result;
        }
    
        public ResultContext RemoveFavoriteSong(User user, int id)
        {
            ResultContext result = new ResultContext();

            if (id > 0 && id <= _songCount)
            {
                FavoriteSong fs = _favSongRepo.Get(user.UserID, id);

                if (fs != null && fs != default(FavoriteSong))
                {
                    if (_favSongRepo.Remove(fs) > 0)
                    {
                        // Updates the average score of the song
                        _songRepo.RemoveScore(id, fs.Score);

                        result.AddData($"The song with id of [{id}] removed successfully from the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot remove the song with id of [{id}] from favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The song with id of [{id}] is already " +
                    $"not in favorites for user {user.Username}!");
            }
            else result.AddExceptionMessage($"'id' parameter must be positive number and " + 
                $"'score' must be between 1 and 10!");

            return result;
        }

        public ResultContext GetFavorites(User user)
        {
            ResultContext result = new ResultContext();

            var favs = _favSongRepo.GetAllByUserID(user.UserID).ToList();

            var temp = favs.Select(fs => 
            {
                var song = _songRepo.Get(fs.SongID);
                song.Anime = _animeRepo.Get(song.AnimeID);
                return ModelConverter.FromSong(song, fs.Score);
            });
            result.AddData(temp);

            return result;
        }

        public ResultContext UpdateFavoriteSongScore(User user, int id, int score)
        {
            ResultContext result = new ResultContext();

            if (id > 0 && id <= _songCount && score > 0 && score <= 10)
            {
                FavoriteSong fs = _favSongRepo.Get(user.UserID, id);

                if (fs != null && fs != default(FavoriteSong))
                {
                    var previousScore = fs.Score;
                    fs.Score = score;

                    if (_favSongRepo.Update(fs) > 0)
                    {
                        // Updates the average score of the song
                        _songRepo.UpdateScore(id, fs.Score, previousScore);

                        result.AddData($"The song with id of [{id}] updated successfully for the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot update the song with id of [{id}] from favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The song with id of [{id}] doesn't " +
                    $"exist in favorites for user {user.Username}!");
            }
            else result.AddExceptionMessage($"'id' parameter must be positive number and " + 
                $"'score' must be between 1 and 10!");

            return result;
        }
    }
}