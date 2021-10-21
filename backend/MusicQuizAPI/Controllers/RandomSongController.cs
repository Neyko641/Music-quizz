﻿﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Extensions;
using MusicQuizAPI.Models.Parameters;

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/song")]
    public class RandomSongController : ControllerBase
    {
        private readonly AnimeService _animeService;
        private readonly UserService _userService;
        private readonly ILogger<RandomSongController> _logger;

        public RandomSongController(AnimeService animeService, ILogger<RandomSongController> logger, UserService userService)
        {
            _animeService = animeService;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("random")]
        public async Task<IActionResult> Random([FromQuery]RandomSongParamModel parameters) 
        {
            var result = new ResultContext<List<DetailedAnimeModel>>();

            result.AddData(await _animeService.GetRandomAnimes(parameters.Count, parameters.Difficulty));
            
            return Ok(result.Result());
        }
    }
}