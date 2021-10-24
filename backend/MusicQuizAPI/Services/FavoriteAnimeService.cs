using System.Linq;
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
        private readonly int _animeCount;

        public FavoriteAnimeService(ILogger<AnimeService> logger, AnimeRepository animeRepo,
            FavoriteAnimeRepository favAnimeRepo)
        {
            _logger = logger;
            _animeRepo = animeRepo;
            _favAnimeRepo = favAnimeRepo;
            _animeCount = _animeRepo.Count;
        }

        public ResultContext AddFavoriteAnime(User user, int id)
        {
            ResultContext result = new ResultContext();

            if (id >= 0 && id <= _animeCount)
            {
                if (!_favAnimeRepo.Exist(user.UserID, id))
                {
                    FavoriteAnime fa = new FavoriteAnime
                    {
                        UserID = user.UserID,
                        AnimeID = id,
                    };

                    if (_favAnimeRepo.Add(fa) > 0)
                    {
                        result.AddData($"The anime with id of [{id}] " +
                        $"was added successfully to the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot add anime with id of [{id}] in favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The anime with id of [{id}] " + 
                    $"is already in favorites for user {user.Username} or doesn't exist!");
            }
            else result.AddExceptionMessage($"'id' parameter is required and must be positive number!");

            return result;
        }
    
        public ResultContext RemoveFavoriteAnime(User user, int id)
        {
            ResultContext result = new ResultContext();

            if (id >= 0 && id <= _animeCount)
            {
                FavoriteAnime fa = _favAnimeRepo.Get(user.UserID, id);

                if (fa != null && fa != default(FavoriteAnime))
                {
                    if (_favAnimeRepo.Remove(fa) > 0)
                    {
                        result.AddData($"The anime with id of [{id}] removed successfully from the user {user.Username}!");
                    }   
                    else result.AddExceptionMessage($"Something went wrong. " + 
                        $"Cannot remove the anime with id of [{id}] from favorites for user {user.Username}!");
                }
                else result.AddExceptionMessage($"The anime with id of [{id}] is already " +
                    $"not in favorites for user {user.Username}!");
            }
            else result.AddExceptionMessage($"'id' parameter is required and must be positive number!");

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