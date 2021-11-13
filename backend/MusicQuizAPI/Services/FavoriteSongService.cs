using System.Linq;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;
using System.Collections.Generic;

namespace MusicQuizAPI.Services
{
    public class FavoriteSongService
    {
        private readonly ILogger<FavoriteSongService> _logger;
        private readonly SongRepository _songRepo;
        private readonly FavoriteSongRepository _favSongRepo;
        private readonly AnimeRepository _animeRepo;

        public FavoriteSongService(ILogger<FavoriteSongService> logger, SongRepository songRepo,
            FavoriteSongRepository favSongRepo, AnimeRepository animeRepo)
        {
            _logger = logger;
            _songRepo = songRepo;
            _favSongRepo = favSongRepo;
            _animeRepo = animeRepo;
        }

        public bool Exist(FavoriteSong favoriteSong)
            => _favSongRepo.Exist(favoriteSong);

        public bool AddFavorite(FavoriteSong favoriteSong)
        {
            if (!Exist(favoriteSong))
            {
                if (_favSongRepo.Add(favoriteSong) > 0)
                {
                    // Updates the average score of the song
                    _songRepo.AddScore(favoriteSong.SongID, favoriteSong.Score);

                    return true;
                }   
                
                // It shouldn't be getting here at any condition 
                return false;
            }
            else return false;
        }


        public bool RemoveFavorite(FavoriteSong favoriteSong)
        {
            FavoriteSong favoriteSongToRemove = _favSongRepo.Get(favoriteSong.UserID, favoriteSong.SongID);

            if (favoriteSongToRemove != null && favoriteSongToRemove != default(FavoriteSong))
            {
                if (_favSongRepo.Remove(favoriteSongToRemove) > 0)
                {
                    // Updates the average score of the song
                    _songRepo.RemoveScore(favoriteSongToRemove.SongID, favoriteSongToRemove.Score);

                    return true;
                }   

                // It shouldn't be getting here at any condition 
                return false;
            }
            
            return false;
        }


        public bool UpdateFavorite(FavoriteSong favoriteSong)
        {
            FavoriteSong favoriteSongToUpdate = _favSongRepo.Get(favoriteSong.UserID, favoriteSong.SongID);

            if (favoriteSongToUpdate != null && favoriteSongToUpdate != default(FavoriteSong))
            {
                int previousScore = favoriteSongToUpdate.Score;
                favoriteSongToUpdate.Score = favoriteSong.Score;

                if (_favSongRepo.Update(favoriteSongToUpdate) > 0)
                {
                    // Updates the average score of the song
                    _songRepo.UpdateScore(favoriteSongToUpdate.SongID, 
                        favoriteSongToUpdate.Score, previousScore);

                    return true;
                }   
                
                // It shouldn't be getting here at any condition 
                return false;
            }
            
            return false;
        }


        public List<FavoriteSong> GetFavorites(User user)
        {
            List<FavoriteSong> favs = _favSongRepo.GetAllByUserID(user.UserID).ToList();

            favs.ForEach(fs =>
            {
                fs.Song = _songRepo.Get(fs.SongID);
                fs.Song.Anime = _animeRepo.Get(fs.Song.AnimeID);
            });

            return favs;
        }
    }
}