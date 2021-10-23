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
using Microsoft.Extensions.Configuration;

namespace MusicQuizAPI.Controllers
{
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/init")]
    public class InitializationController : ControllerBase
    {
        private readonly InitialDatabaseService _service;
        private readonly IConfiguration _config;

        public InitializationController(InitialDatabaseService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpGet("top-animes")]
        public IActionResult InitTopAnimes(string secret) 
        {
            if (secret == _config["JWT:Secret"])
            {
                _service.InitializeTopAnimes();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("animes-songs")]
        public IActionResult InitAnimesAndSongs(string secret) 
        {
            if (secret == _config["JWT:Secret"])
            {
                _service.InitializeAnimesAndSongs();
                return Ok();
            }
            return BadRequest();
        }
    }
}