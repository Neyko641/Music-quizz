using System.Collections.Generic;
using System;
using MusicQuizz_backend.Models;
using MusicQuizz_backend.Helpers;


namespace MusicQuizz_backend.Services
{
    public class AnimeService
    {
        private List<AnimeModel> Animes = new List<AnimeModel>();

        public AnimeService()
        {
            Animes = APIHelper.GetAnimes();
        }

        public List<DetailedAnimeModel> GetRandomAnimes(int count)
        {
            List<AnimeModel> result = new List<AnimeModel>();
            Random rnd = new Random();
            int index;
            AnimeModel item;

            // Add random anime from Animes Property to result while count is higher than 0
            // while avoiding duplications
            while (count > 0)
            {
                index = rnd.Next(Animes.Count);
                item = Animes[index];

                if (!result.Contains(item))
                {
                    result.Add(item);
                    count--;
                }
            }

            return APIHelper.GetAnimesDetails(result);;
        }
    }
}