using System.Linq;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Exceptions;
using System.Collections.Generic;


namespace MusicQuizAPI.Services
{
    public class FavoriteAnimeService
    {
        private readonly ILogger<FavoriteAnimeService> _logger;
        private readonly AnimeRepository _animeRepo;
        private readonly FavoriteAnimeRepository _favAnimeRepo;

        public FavoriteAnimeService(ILogger<FavoriteAnimeService> logger, AnimeRepository animeRepo,
            FavoriteAnimeRepository favAnimeRepo)
        {
            _logger = logger;
            _animeRepo = animeRepo;
            _favAnimeRepo = favAnimeRepo;
        }



        public bool Exist(FavoriteAnime favoriteAnime)
            => _favAnimeRepo.Exist(favoriteAnime);


        public void AddFavorite(FavoriteAnime favoriteAnime)
        {
            if (!_animeRepo.Exist(favoriteAnime.AnimeID)) 
            {
                throw new NotExistException($"The anime [{favoriteAnime.AnimeID}] doesn't exist!");
            }  

            if (Exist(favoriteAnime)) 
            {
                throw new AlreadyExistException($"The anime [{favoriteAnime.AnimeID}] " +
                    $"is already in favorites for user [{favoriteAnime.UserID}]");
            } 
            
            if (_favAnimeRepo.Add(favoriteAnime) == 0)
            {
                throw new UnexpectedException($"Cannot add the anime [{favoriteAnime.AnimeID}] " +
                    $"to favorites for user [{favoriteAnime.UserID}]");
            } 
            
            // Updates the average score of the anime if its added
            _animeRepo.AddScore(favoriteAnime.AnimeID, favoriteAnime.Score);    
        }


        public void RemoveFavorite(FavoriteAnime favoriteAnime)
        {
            FavoriteAnime favoriteAnimeToRemove = _favAnimeRepo.Get(favoriteAnime.UserID, favoriteAnime.AnimeID);

            if (favoriteAnimeToRemove == null)
            {
                throw new NotExistException($"The anime [{favoriteAnime.AnimeID}] is already " +
                    $"not in favorites for user {favoriteAnime.UserID}!");
            }
                
            if (_favAnimeRepo.Remove(favoriteAnimeToRemove) == 0)
            {
                throw new UnexpectedException($"Cannot remove the anime [{favoriteAnime.AnimeID}] " +
                    $"from favorites for user [{favoriteAnime.UserID}]");
            }   

            // Updates the average score of the anime
            _animeRepo.RemoveScore(favoriteAnimeToRemove.AnimeID, favoriteAnimeToRemove.Score);
        }


        public void UpdateFavorite(FavoriteAnime favoriteAnime)
        {
            FavoriteAnime favoriteAnimeToUpdate = _favAnimeRepo.Get(favoriteAnime.UserID, favoriteAnime.AnimeID);

            if (favoriteAnimeToUpdate == null)
            {
                throw new NotExistException($"The anime [{favoriteAnime.AnimeID}] is " +
                    $"not in favorites for user {favoriteAnime.UserID}!");
            }
            
            int previousScore = favoriteAnimeToUpdate.Score;
            favoriteAnimeToUpdate.Score = favoriteAnime.Score;

            if (_favAnimeRepo.Update(favoriteAnimeToUpdate) == 0)
            {
                throw new UnexpectedException($"Cannot update the anime [{favoriteAnime.AnimeID}] " +
                    $"from favorites for user [{favoriteAnime.UserID}]");
            }

            // Updates the average score of the anime
            _animeRepo.UpdateScore(favoriteAnimeToUpdate.AnimeID, favoriteAnimeToUpdate.Score, previousScore);
        }


        public List<FavoriteAnime> GetFavorites(int userId)
            => _favAnimeRepo.GetAllByUserID(userId).ToList();

        public int GetFavoriteScore(int userId, int animeId) 
        {
            FavoriteAnime fa =_favAnimeRepo.Get(userId, animeId);

            if (fa != null) return fa.Score;
            else return 0;
        }
    }
}