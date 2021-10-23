﻿using Microsoft.AspNetCore.Mvc;
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
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/song")]
    public class SongController : ControllerBase
    {
        private readonly SongService _songService;
        private readonly UserService _userService;
        private readonly ILogger<SongController> _logger;

        public SongController(SongService songService, ILogger<SongController> logger, UserService userService)
        {
            _songService = songService;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("random")]
        public IActionResult Random([FromQuery]RandomSongParamModel parameters) 
        {
            var result = new ResultContext();

            result.AddData(_songService.GetRandomSongs(parameters.Count, parameters.Difficulty)
                .Select(s => ModelConverter.FromSong(s)));
            
            return Ok(result.Result());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery]SearchSongParamModel parameters)
        {
            var result = new ResultContext();

            result.AddData(_songService.SearchSong(parameters.Value, parameters.SearchType)
                .Select(s => ModelConverter.FromSong(s)));
            
            return Ok(result.Result());
        }
    }
}