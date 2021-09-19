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

        public AnimeService()
        {
            _animes = APIHelper.GetAnimes();
            _topAnimes = APIHelper.GetTopAnimes();
        }

        public List<DetailedAnimeModel> GetRandomAnimes(int count, string difficulty)
        {
            List<AnimeModel> result = new List<AnimeModel>();
            Random rnd = new Random();
            int index;
            AnimeModel item;

            // Add random anime from Animes Property to result while count is higher than 0
            // while avoiding duplications
            while (count > 0)
            {
                index = rnd.Next(_animes.Count);
                item = _animes[index];

                if (!result.Contains(item))
                {
                    if (string.IsNullOrEmpty(item.difficulty))
                    {
                        item.difficulty = GetAnimeDifficulty(item.source);
                    }

                    if (item.difficulty == difficulty)
                    {
                        result.Add(item);
                        count--;
                    }
                }
            }

            return APIHelper.GetAnimesDetails(result);
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