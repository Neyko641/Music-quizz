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
    [Route("/anime")]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeService _animeService;
        private readonly UserService _userService;

        public AnimeController(AnimeService animeService, UserService userService)
        {
            _animeService = animeService;
            _userService = userService;
        }

        [HttpPost(Name = "add-favorite")]
        public async Task<IActionResult> Get([FromQuery]RandomSongParamModel parameters) 
        {
            var result = new ResultContext<List<DetailedAnimeModel>>();

            result.AddData(await _animeService.GetRandomAnimes(parameters.Count, parameters.Difficulty));
            
            return Ok(result.Result());
        }
    }
}