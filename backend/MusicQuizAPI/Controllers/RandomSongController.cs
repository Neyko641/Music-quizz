﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Models.Parameters;
using Microsoft.AspNetCore.Authorization;

namespace MusicQuizAPI.Controllers
{
    //[Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("/song/random")]
    public class RandomSongController : ControllerBase
    {
        private readonly AnimeService _animeService;

        public RandomSongController(AnimeService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]RandomSongParamModel parameters) 
        {
            var result = new ResultContext<List<DetailedAnimeModel>>();

            result.AddData(await _animeService.GetRandomAnimes(parameters.Count, parameters.Difficulty));
            
            return Ok(result.Result());
        }
    }
}