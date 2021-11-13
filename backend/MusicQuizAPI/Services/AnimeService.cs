using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using MusicQuizAPI.Models.API;
using MusicQuizAPI.Helpers;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;

namespace MusicQuizAPI.Services
{
    public class AnimeService
    {
        private readonly AnimeRepository _animeRepo;

        public AnimeService(ILogger<AnimeService> logger, AnimeRepository animeRepo)
        {
            _animeRepo = animeRepo;
        }

        public List<Anime> SearchAnime(string title) 
        {
            List<Anime> animes = new List<Anime>();

            animes = _animeRepo.GetAll(title.ToLower()).ToList();

            return animes;
        }

        public Anime GetAnime(int id)
            => _animeRepo.Get(id);
    }
}