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
        private readonly ILogger<AnimeService> _logger;
        private readonly AnimeRepository _animeRepo;

        public AnimeService(ILogger<AnimeService> logger, AnimeRepository animeRepo)
        {
            _logger = logger;
            _animeRepo = animeRepo;
        }

        
    }
}