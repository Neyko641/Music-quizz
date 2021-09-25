using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MusicQuizAPI.Models;
using MusicQuizAPI.Helpers;
using Microsoft.Extensions.Logging;

namespace MusicQuizAPI.Services
{
    public class AnimeService
    {
        private List<AnimeModel> _animes = new List<AnimeModel>();
        private List<AnimeModel> _topAnimes = new List<AnimeModel>();
        private readonly ILogger<AnimeService> _logger;
        private readonly Random _rnd = new Random();

        public AnimeService(ILogger<AnimeService> logger)
        {
            _logger = logger;
        }

        public void Update(List<AnimeModel> newAnimes, List<AnimeModel> topAnimes)
        {
            _animes = newAnimes;
            _topAnimes = topAnimes;
        }

        public async Task<List<DetailedAnimeModel>> GetRandomAnimes(int count, string difficulty)
        {
            List<AnimeModel> animeList = new List<AnimeModel>();
            AnimeModel anime;
            int index;

            while (count > 0)
            {
                index = _rnd.Next(_animes.Count);
                anime = _animes[index];

                if (!animeList.Contains(anime))
                {
                    if (string.IsNullOrEmpty(anime.difficulty))
                    {
                        anime.difficulty = GetAnimeDifficulty(anime.source);
                    }

                    if (anime.difficulty == difficulty)
                    {
                        animeList.Add(anime);
                        count--;
                    }
                }
            }

            return await APIHelper.GetAnimesDetails(animeList);
        }

        public string GetAnimeDifficulty(string title)
        {
            for (int i = 0; i < _topAnimes.Count; i++)
            {
                if (_topAnimes[i].source == title)
                {
                    return i < 150 ? "easy" : "medium";
                }
            }

            return "hard";
        }
    }
}