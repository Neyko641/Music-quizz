using System.Linq;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;
using System.Net;
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

        public bool AddFavorite(FavoriteAnime favoriteAnime)
        {
            if (!Exist(favoriteAnime))
            {
                if (_favAnimeRepo.Add(favoriteAnime) > 0)
                {
                    // Updates the average score of the anime
                    _animeRepo.AddScore(favoriteAnime.AnimeID, favoriteAnime.Score);

                    return true;
                }  
                
                // It shouldn't be getting here at any condition 
                return false;
            }
            else return false;
        }

        public bool RemoveFavorite(FavoriteAnime favoriteAnime)
        {
            FavoriteAnime favoriteAnimeToRemove = _favAnimeRepo.Get(favoriteAnime.UserID, favoriteAnime.AnimeID);

            if (favoriteAnimeToRemove != null && favoriteAnimeToRemove != default(FavoriteAnime))
            {
                if (_favAnimeRepo.Remove(favoriteAnimeToRemove) > 0)
                {
                    // Updates the average score of the anime
                    _animeRepo.RemoveScore(favoriteAnimeToRemove.AnimeID, favoriteAnimeToRemove.Score);

                    return true;
                }   

                // It shouldn't be getting here at any condition 
                return false;
            }
            
            return false;
        }

        public bool UpdateFavorite(FavoriteAnime favoriteAnime)
        {
            FavoriteAnime favoriteAnimeToUpdate = _favAnimeRepo.Get(favoriteAnime.UserID, favoriteAnime.AnimeID);

            if (favoriteAnimeToUpdate != null && favoriteAnimeToUpdate != default(FavoriteAnime))
            {
                int previousScore = favoriteAnimeToUpdate.Score;
                favoriteAnimeToUpdate.Score = favoriteAnime.Score;

                if (_favAnimeRepo.Update(favoriteAnimeToUpdate) > 0)
                {
                    // Updates the average score of the anime
                    _animeRepo.UpdateScore(favoriteAnimeToUpdate.AnimeID, 
                        favoriteAnimeToUpdate.Score, previousScore);

                    return true;
                }   
                
                // It shouldn't be getting here at any condition 
                return false;
            }
            
            return false;
        }

        public List<FavoriteAnime> GetFavorites(User user)
        {
            List<FavoriteAnime> favs = _favAnimeRepo.GetAllByUserID(user.UserID).ToList();

            return favs;
        }
    }
}