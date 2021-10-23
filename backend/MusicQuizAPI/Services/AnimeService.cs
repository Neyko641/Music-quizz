using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using MusicQuizAPI.Models.API;
using MusicQuizAPI.Helpers;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Services
{
    public class AnimeService
    {
        private readonly ILogger<AnimeService> _logger;
        private readonly Random _rnd = new Random();
        private readonly MusicQuizRepository _repo;
        private int _animesCount;
        private int _songCount;

        public AnimeService(ILogger<AnimeService> logger, MusicQuizRepository repo)
        {
            _logger = logger;
            _repo = repo;
            _animesCount = _repo.AnimesCount;
            _songCount = _repo.SongCount;
        }

        public List<AnimeModel> GetAllAnimesFromAPI()
        {
            var animes = APIHelper.GetAnimes().Result;
            var result = new List<AnimeModel>();
            animes.ForEach(anime => 
            {
                if (anime == null || anime == default(AnimeModel)) return;
                if (string.IsNullOrWhiteSpace(anime.difficulty) && !string.IsNullOrWhiteSpace(anime.source))
                {
                    anime.difficulty = GetAnimeDifficulty(anime.source);
                    result.Add(anime);
                }
            });

            return result;
        }

        private string GetAnimeDifficulty(string title)
        {
            int easyRankLimit = 150;
            int mediumRankLimit = 300;

            if (_repo.ExistTopAnime(title))
            {
                var i = _repo.GetAnimeRank(title);
                return i < easyRankLimit ? "easy" : (i < mediumRankLimit ? "medium" : "hard");
            }
            return "hard";
        }
    
        
    }
}