using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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

        public IEnumerable<SongModel> GetSongs() => _animes.Select(a => a.song);

        public IEnumerable<AnimeModel> SearchSongBySongTitle(string title) 
        {
            return _animes
            .Where(a => 
            {
                if (a != null && a.song != null)
                {
                    return a.song.title.ToLower().Contains(title.ToLower());
                }
                return false;
            })
            .Select(a => 
            {
                a.difficulty = GetAnimeDifficulty(a.source);
                return a;
            });
        }
            

        public IEnumerable<AnimeModel> SearchSongByAnimeTitle(string title) 
        {
            return _animes
            .Where(a => 
            {
                if (a != null && a.source != null && a.song != null)
                {
                    return a.source.ToLower().Contains(title.ToLower());
                }
                return false;
            })
            .Select(a => 
            {
                a.difficulty = GetAnimeDifficulty(a.source);
                return a;
            });
        }

        private string GetAnimeDifficulty(string title)
        {
            /*
            TOP 1-150   => Easy
            TOP 150-300 => Medium
            TOP >300    => Hard
            */

            var i = _topTitles.IndexOf(title);
            return i < 0 ? "hard" : (i < 150 ? "easy" : "medium");
        }
    }
}