﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Models.Parameters;


namespace MusicQuizAPI.Controllers
{
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/init")]
    public class InitializationController : ControllerBase
    {
        private readonly InitialDatabaseService _service;
        private readonly IConfiguration _config;
        private readonly ILogger<InitializationController> _logger;

        public InitializationController(InitialDatabaseService service, IConfiguration config,
            ILogger<InitializationController> logger)
        {
            _logger = logger;
            _service = service;
            _config = config;
        }

        [HttpGet("top-animes")]
        public IActionResult InitTopAnimes(string secret)
        {
            if (secret == _config["JWT:Secret"])
            {
                _service.InitializeTopAnimes();
                _logger.LogInformation("Top Animes are initialized!");

                return Ok("Top Animes are initialized!");
            }
            return BadRequest("Incorrect secret!");
        }

        [HttpGet("animes-songs")]
        public IActionResult InitAnimesAndSongs(string secret)
        {
            if (secret == _config["JWT:Secret"])
            {
                _service.InitializeAnimesAndSongs();
                _logger.LogInformation("Animes and Songs are initialized!");

                return Ok("Top Animes are initialized!");
            }
            return BadRequest("Incorrect secret!");
        }
    }
}