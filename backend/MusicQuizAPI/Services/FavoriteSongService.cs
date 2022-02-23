using System.Linq;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Exceptions;
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

        public void AddFavorite(FavoriteSong favoriteSong)
        {
            if (!_songRepo.Exist(favoriteSong.SongID)) 
            {
                throw new NotExistException($"The song [{favoriteSong.SongID}] doesn't exist!");
            }

            if (Exist(favoriteSong)) 
            {
                throw new AlreadyExistException($"The song [{favoriteSong.SongID}] " +
                    $"is already in favorites for user [{favoriteSong.UserID}]");
            } 

            if (_favSongRepo.Add(favoriteSong) == 0)
            {
                throw new UnexpectedException($"Cannot add the song [{favoriteSong.SongID}] " +
                    $"to favorites for user [{favoriteSong.UserID}]");
            } 

            // Updates the average score of the song if its added
            _songRepo.AddScore(favoriteSong.SongID, favoriteSong.Score);
        }


        public void RemoveFavorite(FavoriteSong favoriteSong)
        {
            FavoriteSong favoriteSongToRemove = _favSongRepo.Get(favoriteSong.UserID, favoriteSong.SongID);

            if (favoriteSongToRemove == null)
            {
                throw new NotExistException($"The song [{favoriteSong.SongID}] is already " +
                    $"not in favorites for user {favoriteSong.UserID}!");
            }
                
            if (_favSongRepo.Remove(favoriteSongToRemove) == 0)
            {
                throw new UnexpectedException($"Cannot remove the song [{favoriteSong.SongID}] " +
                    $"from favorites for user [{favoriteSong.UserID}]");
            }   

            // Updates the average score of the song
            _songRepo.RemoveScore(favoriteSongToRemove.SongID, favoriteSongToRemove.Score);
        }


        public void UpdateFavorite(FavoriteSong favoriteSong)
        {
            FavoriteSong favoriteSongToUpdate = _favSongRepo.Get(favoriteSong.UserID, favoriteSong.SongID);

            if (favoriteSongToUpdate == null)
            {
                throw new NotExistException($"The song [{favoriteSong.SongID}] is " +
                    $"not in favorites for user {favoriteSong.UserID}!");
            }
            
            int previousScore = favoriteSongToUpdate.Score;
            favoriteSongToUpdate.Score = favoriteSong.Score;

            if (_favSongRepo.Update(favoriteSongToUpdate) == 0)
            {
                throw new UnexpectedException($"Cannot update the song [{favoriteSong.SongID}] " +
                    $"from favorites for user [{favoriteSong.UserID}]");
            }

            // Updates the average score of the song
            _songRepo.UpdateScore(favoriteSongToUpdate.SongID, favoriteSongToUpdate.Score, previousScore);
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

        public int GetFavoriteScore(int userId, int animeId) 
        {
            FavoriteSong fs = _favSongRepo.Get(userId, animeId);

            if (fs != null) return fs.Score;
            else return 0;
        }
    }
}