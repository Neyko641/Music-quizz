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

namespace MusicQuizAPI.Controllers
{
    [EnableCors("MusicQuizPolicy")]
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]RandomSongParamModel parameters) 
        {
            var result = new ResultContext<List<DetailedAnimeModel>>();
            var address = ClientHelper.GetClientIPAdress(HttpContext);
            _logger.LogTrace($"[GET] request from {address}!");

            result.AddData(await _animeService.GetRandomAnimes(parameters.Count, parameters.Difficulty));
            _logger.LogTrace($"[GET] (OK) response to {address}!");
            
            return Ok(result.Result());
        }
    }
}