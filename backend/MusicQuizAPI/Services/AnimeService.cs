using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MusicQuizz_backend.Models;
using MusicQuizz_backend.Helpers;


namespace MusicQuizz_backend.Services
{
    public class AnimeService
    {
        private readonly List<AnimeModel> _animes = new List<AnimeModel>();
        private readonly List<AnimeModel> _topAnimes = new List<AnimeModel>();
        private readonly Random _rnd = new Random();

        public AnimeService()
        {
            _animes = APIHelper.GetAnimes();
            _topAnimes = APIHelper.GetTopAnimes();
        }

        public List<DetailedAnimeModel> GetRandomAnimes(int count, string difficulty)
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

            return APIHelper.GetAnimesDetails(animeList);
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