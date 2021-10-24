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
    public class FavoriteAnimeService
    {
        private readonly ILogger<AnimeService> _logger;
        private readonly AnimeRepository _animeRepo;
        private readonly FavoriteAnimeRepository _favAnimeRepo;

        public FavoriteAnimeService(ILogger<AnimeService> logger, AnimeRepository animeRepo,
            FavoriteAnimeRepository favAnimeRepo)
        {
            _logger = logger;
            _animeRepo = animeRepo;
            _favAnimeRepo = favAnimeRepo;
        }

        public ResultContext AddFavoriteAnime(User user, string title)
        {
            ResultContext result = new ResultContext();

            if (!string.IsNullOrWhiteSpace(title))
            {
                Anime anime = _animeRepo.Get(title);

                if (anime != null && anime != default(Anime))
                {
                    if (!_favAnimeRepo.Exist(user.UserID, anime.AnimeID))
                    {
                        FavoriteAnime fa = new FavoriteAnime
                        {
                            UserID = user.UserID,
                            AnimeID = anime.AnimeID,
                        };

                        if (_favAnimeRepo.Add(fa) > 0)
                        {
                            result.AddData($"'{title}' added successfully to the user {user.Username}!");
                        }   
                        else result.AddExceptionMessage($"Something went wrong." + 
                            $"Cannot add anime '{title}' in favorites for user {user.Username}!");
                    }
                    else result.AddExceptionMessage($"Anime '{title}' is already in favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"Cannot find anime with title '{title}'!");
            }
            else result.AddExceptionMessage($"'title' parameter is required!");

            return result;
        }
    
        public ResultContext RemoveFavoriteAnime(User user, string title)
        {
            ResultContext result = new ResultContext();

            if (!string.IsNullOrWhiteSpace(title))
            {
                Anime anime = _animeRepo.Get(title);

                if (anime != null && anime != default(Anime))
                {
                    FavoriteAnime fa = _favAnimeRepo.Get(user.UserID, anime.AnimeID);

                    if (fa != null && fa != default(FavoriteAnime))
                    {
                        if (_favAnimeRepo.Remove(fa) > 0)
                        {
                            result.AddData($"'{title}' removed successfully from the user {user.Username}!");
                        }   
                        else result.AddExceptionMessage($"Something went wrong." + 
                            $"Cannot remove anime '{title}' from favorites for user {user.Username}!");
                    }
                    else result.AddExceptionMessage($"Anime '{title}' is already not in favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"Cannot find anime with title '{title}'!");
            }
            else result.AddExceptionMessage($"'title' parameter is required!");

            return result;
        }

        public ResultContext GetFavorites(User user)
        {
            ResultContext result = new ResultContext();

            var favs = _favAnimeRepo.GetAllByUserID(user.UserID).ToList();

            result.AddData(favs.Select(fa => ModelConverter.FromAnime(_animeRepo.Get(fa.AnimeID))));

            return result;
        }
    }
}