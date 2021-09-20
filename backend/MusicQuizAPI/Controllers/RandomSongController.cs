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
    [Route("/song/random")]
    public class RandomSongController : ControllerBase
    {
        private readonly ILogger<RandomSongController> _logger;
        private readonly AnimeService _animeService;

        public RandomSongController(ILogger<RandomSongController> logger, AnimeService animeService)
        {
            _logger = logger;
            _animeService = animeService;
        }

        [EnableCors("MusicQuizPolicy")]
        [HttpGet]
        public IActionResult Get(string count, string difficulty = "easy") 
        {
            var result = new ResultModel<List<DetailedAnimeModel>>();
            int countInt;

            // count parameter check
            if (!Int32.TryParse(count, out countInt) || countInt < 1 || countInt > 100)
            {
                result.AddExceptionMessage($"[count] is given '{count}', but it must be in the range from 1 to 100.");
            }

            // difficulty parameter check
            if (!(difficulty == "easy" || difficulty == "medium" || difficulty == "hard"))
            {
                result.AddExceptionMessage($"[difficulty] is given '{difficulty}', but it must be easy, medium or hard");
            }

            if (result.StatusCode == 200)
            {
                result.AddData(_animeService.GetRandomAnimes(countInt, difficulty));
                return Ok(result.Result());
            }
            
            return BadRequest(result.Result());
        }
    }
}