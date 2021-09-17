﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using MusicQuizz_backend.Models;
using MusicQuizz_backend.Services;

namespace MusicQuizz_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongListController : ControllerBase
    {
        private readonly ILogger<SongListController> _logger;
        private readonly AnimeService _animeService;

        public SongListController(ILogger<SongListController> logger, AnimeService animeService)
        {
            _logger = logger;
            _animeService = animeService;
        }

        [EnableCors("MusicQuizPolicy")]
        [HttpGet]
        public IEnumerable<DetailedAnimeModel> Get(int count) => _animeService.GetRandomAnimes(count);
    }
}