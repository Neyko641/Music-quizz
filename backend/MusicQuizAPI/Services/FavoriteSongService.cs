using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using MusicQuizAPI.Models.API;
using MusicQuizAPI.Helpers;
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

        public ResultContext AddFavoriteSong(User user, int id)
        {
            ResultContext result = new ResultContext();

            if (id > 0 && id <= _songCount)
            {
                if (!_favSongRepo.Exist(user.UserID, id))
                {
                    FavoriteSong fs = new FavoriteSong
                    {
                        UserID = user.UserID,
                        SongID = id,
                    };

                    if (_favSongRepo.Add(fs) > 0)
                    {
                        result.AddData($"The song with id of [{id}] " +
                        $"was added successfully to the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot add song with id of [{id}] in favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The song with id of [{id}] " + 
                    $"is already in favorites for user {user.Username} or doesn't exist!");
            }
            else result.AddExceptionMessage($"'id' parameter is required and must be positive number!");

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
                        result.AddData($"The song with id of [{id}] removed successfully from the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot remove the song with id of [{id}] from favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The song with id of [{id}] is already " +
                    $"not in favorites for user {user.Username}!");
            }
            else result.AddExceptionMessage($"'id' parameter is required and must be positive number!");

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
                return ModelConverter.FromSong(song);
            });
            result.AddData(temp);

            return result;
        }
    }
}