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
        private static List<AnimeModel> _animes = new List<AnimeModel>();
        private static List<string> _topTitles = new List<string>();
        private readonly ILogger<AnimeService> _logger;
        private readonly Random _rnd = new Random();

        public AnimeService(ILogger<AnimeService> logger)
        {
            _logger = logger;
        }

        public static void Update()
        {
            _animes = APIHelper.GetAnimes().Result;
            _topTitles = APIHelper.GetTopTitles();

            FileHelper.WriteToAnimes(_animes);
            FileHelper.WriteToTopAnimes(_topTitles);
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
                        _animes.Insert(index, anime);
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

        private string GetAnimeDifficulty(string title)
        {
            /*
            TOP 1-150   => Easy
            TOP 150-300 => Medium
            TOP >300    => Hard
            */

            for (int i = 0; i < _topTitles.Count; i++)
            {
                if (_topTitles[i] == title)
                {
                    return i < 150 ? "easy" : "medium";
                }
            }

            return "hard";
        }
    }
}